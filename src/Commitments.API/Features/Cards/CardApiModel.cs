using Commitments.Core.Entities;

namespace Commitments.API.Features.Cards
{
    public class CardApiModel
    {        
        public int CardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static CardApiModel FromCard(Card card)
        {
            var model = new CardApiModel();
            model.CardId = card.CardId;
            model.Name = card.Name;
            model.Description = card.Description;
            return model;
        }
    }
}
