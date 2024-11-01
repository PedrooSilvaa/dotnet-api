using Flunt.Validations;
using IWantApp.Domain.Products;

namespace IWantApp.Domain.Products {
    public class Category : Entity{

        public string Name { get; set; }
        public bool Active { get; set; }

        public Category(string name, string createBy, string editedBy) {
            var contract = new Contract<Category>()
                 .IsNotNullOrEmpty(name, "Name", "Name should not be null or empty")
                 .IsGreaterOrEqualsThan(name, 3, "Name")
                 .IsNotNullOrEmpty(createBy, "Name", "Name should not be null or empty")
                 .IsNotNullOrEmpty(editedBy, "Name", "Name should not be null or empty");
            AddNotifications(contract);

            Name = name;
            Active = true;
        }

    }
}



