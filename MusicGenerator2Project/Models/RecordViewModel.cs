namespace MusicGenerator2Project.Models
{
    public class RecordViewModel : MusicGenerator.BLL.Models.MusicItem
    {
        public string DurationStr () => 
            $"{DurationSeconds / 60}:{(DurationSeconds % 60).ToString("D2")}";
    }
}
