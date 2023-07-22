using System.ComponentModel.DataAnnotations;


namespace DataManagementNet.Models
{
    /// <summary>
    ///  This class represents the data structure used for editing contacts in the database. The properties 
    ///  CID, ContactsName, Phone, and Email are defined within the class
    /// </summary>
    public class RCModel
    {
        // The property "CID" represents the identification number of the contact
        public int CID { get; set; }

        // The property "ContactsName" represents the name of the contact
        // The [Required] attribute specifies that this field is required and cannot be NULL
        // ErrorMessage provides the error message to display if the field is not filled
        [Required(ErrorMessage = "Name field is required")]
        public string? ContactsName { get; set; }

        // The property "Phone" represents the phone number of the contact
        // The [Required] attribute specifies that this field is required and cannot be NULL
        // ErrorMessage provides the error message to display if the field is not filled
        [Required(ErrorMessage = "The Phone field is required")]
        public string? Phone { get; set; }

        // The property "Email" represents the email address of the contact
        // The [Required] attribute specifies that this field is required and cannot be NULL
        // ErrorMessage provides the error message to display if the field is not filled
        [Required(ErrorMessage = "The Mail field is required")]
        public string? Email { get; set; }
    }
}
