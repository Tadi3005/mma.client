namespace Mma.Client.Presentations;

public interface IMainViewModel
{
    IStateRoomViewModel StateRoomViewModel { get; set; }

    IDailyScheduleViewModel DailyScheduleViewModel { get; set; }
}
