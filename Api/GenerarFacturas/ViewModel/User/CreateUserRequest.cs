using ViewModel.Validator;

namespace ViewModel.User
{
    public  class CreateUserRequest
    {
        [Validator(true, 100)]
        public string Email { get; set; }

        [Validator(true, 100)]
        public string Password { get; set; }

        [Validator(true, 100)]
        public string FirstName { get; set; }

        [Validator(true, 100)]
        public string LastName { get; set; }

     
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
