using Kumeleme_Solution_WinForm.Datasets;

namespace Kumeleme_Solution_WinForm.Models
{
    public class Data
    {
        public string Name { get; set; }
        public double OperasyondakiHastaYasi { get; set; }
        public double PozitifAksillerLenfNoduAdeti { get; set; }
        public string HayattaKalmaSuresi { get; set; }
        public string Cluster { get; set; }

        public Data()
        {
            int count = Datas.Items.Count == 0 ? 1 : Datas.Items.Count + 1;
            if (string.IsNullOrWhiteSpace(Name))
                Name = "X" + count;
        }
    }
}
