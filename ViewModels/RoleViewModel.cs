using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<OperatorRoleViewModel> OperatorRoles { get; set; }
    }
}
