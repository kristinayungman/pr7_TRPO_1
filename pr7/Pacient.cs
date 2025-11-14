namespace pr7
{
    public class Pacient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime LastAppointment { get; set; }

        public int LastDoctor { get; set; }
        public string Diagnosis { get; set; }
        public string Recomendations { get; set; }
    }
}
