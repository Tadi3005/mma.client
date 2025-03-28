﻿namespace Mma.Client.Domains.AskReservation.Validator;

public class PastDateValidator : IReservationValidator
{
    public ReservationStatus Validate(ReservationRequest request, IList<Reservation> reservations, IList<User> users, Room room)
        => request.IsPast ? ReservationStatus.PastDate : ReservationStatus.Accepted;
}
