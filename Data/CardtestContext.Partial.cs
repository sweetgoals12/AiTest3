using Microsoft.EntityFrameworkCore;

namespace aitest3.Data;

public partial class CardtestContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        // Configure IdentityPasskeyData if it exists
        try
        {
            var passkeyType = System.Type.GetType("Microsoft.AspNetCore.Identity.IdentityPasskeyData, Microsoft.AspNetCore.Identity");
            if (passkeyType != null)
            {
                var entityType = modelBuilder.Model.FindEntityType(passkeyType);
                if (entityType != null)
                {
                    modelBuilder.Entity(passkeyType).HasNoKey();
                }
            }
        }
        catch
        {
            // IdentityPasskeyData may not exist in all versions
        }
    }
}
