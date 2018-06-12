using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using PhilBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PhilBot.Dialogs
{
    [LuisModel("6dcdc2de-e776-475d-9f63-3f192a91f325", "8cedf3fccc2e458983a3d27552fb728e")]
    [Serializable]
    public class LUISDialog : LuisDialog<RoomReservation>
    {
        private readonly BuildFormDelegate<RoomReservation> ReserveRoom;


        public LUISDialog(BuildFormDelegate<RoomReservation> reserveRoom)
        {
            this.ReserveRoom = reserveRoom;
        }
                

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry I don't know what you mean.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            context.Call(new GreetingDialog(), Callback);
        }

        [LuisIntent("Reservation")]
        public async Task RoomReservation(IDialogContext context, LuisResult result)
        {
            var enrollmentForm = new FormDialog<RoomReservation>(new RoomReservation(), this.ReserveRoom, FormOptions.PromptInStart);
            context.Call<RoomReservation>(enrollmentForm, Callback);
        }

        [LuisIntent("QueryAmenities")]
        public async Task QueryAmenities(IDialogContext context, LuisResult result)
        {
            foreach (var entity in result.Entities.Where(Entity => Entity.Type == "Amenity"))
            {
                var value = entity.Entity.ToLower();
                if (value == "pool" || value == "gym" || value == "wifi" || value == "towels")
                {
                    await context.PostAsync("Yes we have that!");
                    context.Wait(MessageReceived);
                    return;
                }
                else
                {
                    await context.PostAsync("Nope, we don't have that.");
                    context.Wait(MessageReceived);
                    return;
                }
            }
                await context.PostAsync("I'm sorry we don't have that.");
                context.Wait(MessageReceived);
                return;
        }



        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }
    }
}