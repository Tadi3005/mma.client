namespace Mma.Client.Domains.AskReservation;

public enum ReservationStatus
{
    None,
    Accepted,
    UserNotFound,
    RoomCapacityExceeded,
    RoomNotAvailable,
    EndBeforeStart,
    OutsideOpeningHours,
    PastDate,
    HoursExactly,
    DescriptionTooShort,
}
