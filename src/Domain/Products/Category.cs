using Flunt.Validations;
using IWantApp.Domain.Products;

namespace IWantApp.Domain.Products {
    public class Category : Entity{

        public string Name { get; private set; }
        public bool Active { get; private set; }

        public Category(string name, string createdBy, string editedBy) {
            Name = name;
            Active = true;
            CreatedBy = createdBy;
            EditedBy = editedBy;
            CreatedOn = DateTime.Now;
            EditedOn = DateTime.Now;

            Validate();
        }

        private void Validate() {
            var contract = new Contract<Category>()
                             .IsNotNullOrEmpty(Name, "Name", "Name should not be null or empty")
                             .IsGreaterOrEqualsThan(Name, 3, "Name")
                             .IsNotNullOrEmpty(CreatedBy, "CreatedBy", "CreateBy should not be null or empty")
                             .IsNotNullOrEmpty(EditedBy, "EditedBy", "EditedBy should not be null or empty");
            AddNotifications(contract);
        }
        
        public void EditInfo(string name, bool active, string editedBy) {
            Name = name;
            Active = active;
            EditedBy = editedBy;
            EditedOn = DateTime.Now;
            Validate();
        }
    }
}



