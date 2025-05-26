namespace WorkshopManager.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int RokProdukcji { get; set; }
        public string CoNaprawic { get; set; }
        public string UserEmail { get; set; } // email przypisanego użytkownika
    }
}

