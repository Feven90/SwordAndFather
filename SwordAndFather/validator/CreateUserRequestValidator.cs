using SwordAndFather.Models;

namespace SwordAndFather.Controllers
{
    // created by user


    public class CreateUserRequestValidator
    {
        public bool Validate(CreateUserRequest requestToValidate)
        {
            return !(string.IsNullOrEmpty(requestToValidate.Username)
                || string.IsNullOrEmpty(requestToValidate.Password));
        }
    }
}
