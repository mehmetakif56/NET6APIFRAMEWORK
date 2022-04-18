namespace TTBS.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { Username = "jason_admin", EmailAddress = "akif.admin@email.com", Password = "MyPass_w0rd", GivenName = "Akif", Surname = "Aydın", Role = "Administrator" },
            new UserModel() { Username = "jason_director", EmailAddress = "metin.director@email.com", Password = "MyPass_w0rd", GivenName = "Metin", Surname = "Aydın", Role = "Director" },
        };
    }
}
