using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using aitest3.Data;
using aitest3.Models;

namespace aitest3.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly CardtestContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(CardtestContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public string Name { get; set; }

        public string DisplayName { get; set; }
        public List<NameEntry> NameEntries { get; set; } = new();
        public List<UserCardDto> UserCards { get; set; } = new();
        public string CurrentUserId { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                CurrentUserId = user.Id;
                // Load cards for the current user with card details
                LoadUserCards();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                CurrentUserId = user.Id;

                if (!string.IsNullOrWhiteSpace(Name))
                {
                    DisplayName = Name;
                }

                // Reload user's cards
                LoadUserCards();

                // Clear the input field
                Name = string.Empty;
            }

            return Page();
        }

        private void LoadUserCards()
        {
            var userCards = _context.UserCards
                .Where(uc => uc.UserId == CurrentUserId)
                .OrderByDescending(uc => uc.AddedAt)
                .ToList();

            UserCards = new List<UserCardDto>();

            foreach (var userCard in userCards)
            {
                var card = _context.Cards.FirstOrDefault(c => c.Id == userCard.CardId);
                if (card != null)
                {
                    UserCards.Add(new UserCardDto
                    {
                        Id = userCard.Id,
                        CardNumber = card.CardNumber,
                        PlayerName = card.PlayerName,
                        TeamCity = card.TeamCity,
                        TeamName = card.TeamName,
                        SetName = card.SetName,
                        Rookie = card.Rookie,
                        ShortPrint = card.ShortPrint,
                        AddedAt = userCard.AddedAt,
                        Count = userCard.Count
                    });
                }
            }
        }
    }

    public class UserCardDto
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string PlayerName { get; set; }
        public string TeamCity { get; set; }
        public string TeamName { get; set; }
        public string SetName { get; set; }
        public bool? Rookie { get; set; }
        public bool? ShortPrint { get; set; }
        public DateTime AddedAt { get; set; }
        public int Count { get; set; }
    }
}
