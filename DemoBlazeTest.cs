using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;


namespace DemoBlazeTest;

public class Tests : PageTest
{
    [Test]
    public async Task HomePageHasCart()
    {
        await Page.GotoAsync("https://www.demoblaze.com/");

        // Create a locator for Cart
        var cart = Page.GetByRole(AriaRole.Link, new() { Name = "Cart" });

        // Expect Cart to contain link to corresponding Cart page
        await Expect(cart).ToHaveAttributeAsync("id", "cartur");

        // Click on the link
        await cart.ClickAsync();

        // Will check that the "cart" page has the correct heading which should say "Products"
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Products" })).ToBeVisibleAsync();

    }

    [Test]
    public async Task HomePageAllowsUserToViewProducts()
    {
        
        await Page.GotoAsync("https://www.demoblaze.com/");

        // Create Locator for "Samsung galaxy s6"
        var samsunggs6 = Page.GetByRole(AriaRole.Link, new() { Name = "Samsung galaxy s6" });

        // Expect the Samsung galaxy s6 to link to the corresponding product page
        await Expect(samsunggs6).ToHaveAttributeAsync("href", "prod.html?idp_=1");

        // Click on the "Samsung galaxy s6" item on the home page
        await samsunggs6.ClickAsync();

        // Should show that the item page has the heading "Samsung galaxy s6"
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Samsung galaxy s6" })).ToBeVisibleAsync();
    }

    [Test]
    public async Task CartHasSelectedItemInCart()
    {
        await Page.GotoAsync("https://www.demoblaze.com/");

        // Create Locator for "Samsung galaxy s6"
        var samsunggs6 = Page.GetByRole(AriaRole.Link, new() { Name = "Samsung galaxy s6" });

        // Expect the "Samsung galaxy s6" to link to the corresponding product page
        await Expect(samsunggs6).ToHaveAttributeAsync("href", "prod.html?idp_=1");

        // Click on the "Samsung galaxy s6" from the home page
        await samsunggs6.ClickAsync();

        // Should be taken to the "Samsung galaxy s6" item page
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Samsung galaxy s6" })).ToBeVisibleAsync();

        // Creates locator for add to cart button
        var addToCart = Page.GetByRole(AriaRole.Link, new() { Name = "Add to cart" });

        // Checks to see if the add to cart button has the correct on click function
        await Expect(addToCart).ToHaveAttributeAsync("onClick", "addToCart(1)");

        // Clicks on add to cart
        await addToCart.ClickAsync();
        
        // Create locator for cart nav bar link
        var cartNav = Page.GetByRole(AriaRole.Link, new() { Name = "Cart", Exact = true });

        // Checks the cart nav bar link has the correct url
        await Expect(cartNav).ToHaveAttributeAsync("href", "cart.html");

        // Clicks on Cart
        await cartNav.ClickAsync();

        // Checks for header called Products on cart page
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Products" })).ToBeVisibleAsync();

        // Creates locator for added basket item (Samsung galaxy s6) 
        var itemInBasket = Page.GetByRole(AriaRole.Cell, new() { Name = "samsung galaxy s6" });

        // Checks item in basket is correct
        await Expect(itemInBasket).ToHaveTextAsync("Samsung galaxy s6");


    }
}


