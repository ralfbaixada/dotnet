using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Extensions
{
    public static class StringExtension
    {
        public static string OnlyNumbers(this string value)
        {
            return value.OnlyNumbers(0, '0');
        }

        public static string OnlyNumbers(this string value, int padLeft, char padChar)
        {
            return Regex.Replace(value ?? "", @"[^\d]", "").PadLeft(padLeft, padChar);
        }

        public static bool IsCpf(this string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf[..9];
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        public static bool IsCnpj(this string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;

            string tempCnpj = cnpj[..12];
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public static bool IsCpfCnpjOrNull(this string document)
        {
            if (document == null)
                return true;
            return document.IsCpfCnpj();
        }

        public static bool IsCpfCnpj(this string document)
        {
            if (document.Length <= 11) return document.IsCpf();
            return document.IsCnpj();
        }

        public static string ObfscateJson(this string json)
        {
            try
            {
                int showLast = 4;
                var regex = new Regex(@"\""(number|creditcardnumber|cvv|cartaocvv|cartaonumero|cnpj|cpf|document|numbercard|cpfcnpj)\""\s*?:\s*?\""(.*?)\""", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                foreach (Match match in regex.Matches(json))
                {
                    if (match.Success)
                    {
                        var original = match.Groups[2].Value;
                        var toShow = original.Length >= showLast ? showLast : original.Length;
                        json = original.Length > 0 ? json.Replace(original, original.Substring(original.Length - toShow, toShow).PadLeft(original.Length, '*')) : json;
                    }
                }
            }
            catch (Exception) { }
            return json;
        }

        public static string FormatLogSize(this string model)
        {
            return model;
            //if (model == null) return null;
            //return (model.Length > 8192) ? "Request too long" : model;
        }

        public static string GetC4uHash(this string value)
        {
            using var sha512Hash = SHA512.Create();
            byte[] data = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static string ToUrl(this string value)
        {
            if (value == null)
                return null;
            return HttpUtility.UrlEncode(value);
        }

        public static string[] ToArray(this string value) => value.Split(',');

        public static List<TEnum> ToListEnum<TEnum>(this string source) where TEnum : struct
        {
            var result = new List<TEnum>();

            foreach (var item in source.ToArray())
            {
                if (Enum.TryParse<TEnum>(item.Trim(), out var sourceEnum))
                    result.Add(sourceEnum);
            }

            return result;
        }

        public static Stream ToStream(this string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            return new MemoryStream(bytes);
        }

        public static async Task<string> ToBase64(this string link)
        {
            try
            {
                var http = new HttpClient();
                var file = await http.GetByteArrayAsync(link);
                return Convert.ToBase64String(file);
            }
            catch (Exception)
            {
                return link;
            }
        }

        public static async Task<Stream> GetStreamAsync(this string link)
        {
            if (link == null)
                return null;
            var http = new HttpClient();
            return await http.GetStreamAsync(link);
        }

        public static string GetMessage(this List<string> listMessages)
        {
            string message = string.Empty;

            foreach (var item in listMessages)
                message += $"{item} {Environment.NewLine}";

            return message;
        }

        public static string ToBase64UrlImage(this string url)
        {
            byte[] data;
            using (var webClient = new WebClient())
            {
                data = webClient.DownloadData(url);
            }
            return $"data:image/png;base64,{Convert.ToBase64String(data)}";
        }

        public static T FromJson<T>(this string json) =>
            JsonConvert.DeserializeObject<T>(json);

        public static string RemoveAccents(this string text)
        {
            StringBuilder result = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    result.Append(letter);
            }
            return result.ToString();
        }
    }
}
