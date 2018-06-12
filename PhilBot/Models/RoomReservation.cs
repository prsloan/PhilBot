using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhilBot.Models
{
    public enum BedSizeOptions
    {
        King,
        Queen,
        Single,
        Double
    }

    public enum AmmenitiesOptions
    {
        Kitchen,
        ExtraTowls,
        GymAccess,
        WiFi,
        UnlimitedSprite
    }


    [Serializable]
    public class RoomReservation
    {
        public BedSizeOptions? BedSize;
        public int? NumberOfOccupants;
        public DateTime? CheckInDate;
        public int? NumberOfDaysToStay;
        public List<AmmenitiesOptions> Ammenities;

        public static IForm<RoomReservation> BuildForm()
        {
            return new FormBuilder<RoomReservation>().Message("Welcom to the hotel reservation bot!").Build();
        }
    }


}