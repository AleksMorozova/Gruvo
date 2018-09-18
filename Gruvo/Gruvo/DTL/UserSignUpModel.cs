namespace Gruvo.DTL
{
    public class UserSignUpModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string SendedVerificationCode { get; set; }
        public string VerificationCodeInput { get; set; }
    }
}
