using FluentValidation.Resources;

namespace Localization
{
    public class CustomLanguageManager : LanguageManager
    {
        public CustomLanguageManager()
        {
            AddTranslation("en-US", "NotNullValidator", "Required field.");
            AddTranslation("en-US", "NotEmptyValidator", "Required field.");

            AddTranslation("pt-BR", "EmailValidator", "Endereço de email inválido.");
            AddTranslation("pt-BR", "GreaterThanOrEqualValidator", "Deve ser superior ou igual a '{ComparisonValue}'.");
            AddTranslation("pt-BR", "GreaterThanValidator", "Deve ser superior a '{ComparisonValue}'.");
            AddTranslation("pt-BR", "LengthValidator", "Deve ter entre {MinLength} e {MaxLength} caracteres. Você digitou {TotalLength} caracteres.");
            AddTranslation("pt-BR", "MinimumLengthValidator", "Deve ser maior ou igual a {MinLength} caracteres. Você digitou {TotalLength} caracteres.");
            AddTranslation("pt-BR", "MaximumLengthValidator", "Deve ser menor ou igual a {MaxLength} caracteres. Você digitou {TotalLength} caracteres.");
            AddTranslation("pt-BR", "LessThanOrEqualValidator", "Deve ser inferior ou igual a '{ComparisonValue}'.");
            AddTranslation("pt-BR", "LessThanValidator", "Deve ser inferior a '{ComparisonValue}'.");
            AddTranslation("pt-BR", "NotEmptyValidator", "Campo obrigatório.");
            AddTranslation("pt-BR", "NotEqualValidator", "Deve ser diferente de '{ComparisonValue}'.");
            AddTranslation("pt-BR", "NotNullValidator", "Campo obrigatório.");
            AddTranslation("pt-BR", "PredicateValidator", "Não atende a condição definida.");
            AddTranslation("pt-BR", "AsyncPredicateValidator", "Não atende a condição definida.");
            AddTranslation("pt-BR", "RegularExpressionValidator", "Não está no formato correto.");
            AddTranslation("pt-BR", "EqualValidator", "Deve ser igual a '{ComparisonValue}'.");
            AddTranslation("pt-BR", "ExactLengthValidator", "Deve ter no máximo {MaxLength} caracteres. Você digitou {TotalLength} caracteres.");
            AddTranslation("pt-BR", "ExclusiveBetweenValidator", "Deve, exclusivamente, estar entre {From} e {To}. Você digitou {Value}.");
            AddTranslation("pt-BR", "InclusiveBetweenValidator", "Deve estar entre {From} e {To}. Você digitou {Value}.");
            AddTranslation("pt-BR", "CreditCardValidator", "Não é um número válido de cartão de crédito.");
            AddTranslation("pt-BR", "ScalePrecisionValidator", "Não pode ter mais do que {ExpectedPrecision} dígitos no total, com {ExpectedScale} dígitos decimais. {Digits} dígitos e {ActualScale} decimais foram informados.");
            AddTranslation("pt-BR", "EmptyValidator", "Deve estar vazio.");
            AddTranslation("pt-BR", "NullValidator", "Deve estar null.");
            AddTranslation("pt-BR", "EnumValidator", "Possui um intervalo de valores que não inclui '{PropertyValue}'.");
            AddTranslation("pt-BR", "Length_Simple", "Deve ter entre {MinLength} e {MaxLength} caracteres.");
            AddTranslation("pt-BR", "MinimumLength_Simple", "Deve ser maior ou igual a {MinLength} caracteres.");
            AddTranslation("pt-BR", "MaximumLength_Simple", "Deve ser menor ou igual a {MaxLength} caracteres.");
            AddTranslation("pt-BR", "ExactLength_Simple", "Deve ter {MaxLength} caracteres.");
            AddTranslation("pt-BR", "InclusiveBetween_Simple", "Deve estar entre {From} e {To}.");
        }
    }
}
