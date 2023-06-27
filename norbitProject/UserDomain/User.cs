namespace UserDomain
{
    public class User
    {
        public long Id { get; set; }

        public string Nickname { get; set; } = "";

        public string Login { get; set; } = "";

        public string Password { get; set; } = "";

        public string Email { get; set; } = "";

        public string[] ComplTasks { get; set; }

        public int Rank { get; set; } = 0;

        public int Level { get; set; } = 0;

        public string GH { get; set; } = "";

        public float Balance { get; set; } = 0.0f;
    }
}