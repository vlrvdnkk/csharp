namespace PhoneDirectory
{
    public class Contact
    {
        private Guid id;
        private string name;
        private string phoneNumber;

        public Guid Id
        {
            get { return id; }
            private set { id = value; }
        }

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            private set { phoneNumber = value; }
        }

        public Contact(string name, string phoneNumber)
        {
            Id = Guid.NewGuid();
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}