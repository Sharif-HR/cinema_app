namespace CinemaApp;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

    [TestClass]
public class RefreshmentsTest
{
    [TestMethod]
    public void TestAddRefreshment()
    {
        RefreshmentsLogic refreshmentsLogic = new();
        var fanta = new RefreshmentModel("Fanta", true, 1.99);

        refreshmentsLogic.AddRefreshment(fanta);

        var refreshments = refreshmentsLogic.GetRefreshments();

        bool refreshmentAdded = refreshments.Any(r => r.Id == fanta.Id);

        Assert.AreEqual(true, refreshmentAdded);
    }

    [TestMethod]
    public void TestUpdateRefreshment()
    {
        RefreshmentsLogic refreshmentsLogic = new();
        var refreshments = refreshmentsLogic.GetRefreshments();

        int drinkIndex = 0;
        double newDrinkPrice = 12.95;

        refreshmentsLogic.EditRefreshment(index: drinkIndex, propertyName: "Price", updatedValue: newDrinkPrice);


        bool updatedDrink = refreshments[drinkIndex].Price == newDrinkPrice;

        Assert.AreEqual(true, updatedDrink);
    }

    [TestMethod]
    public void TestDeleteRefreshment()
    {
        RefreshmentsLogic refreshmentsLogic = new();
        var refreshments = refreshmentsLogic.GetRefreshments();

        var oldDrink = refreshments[0];

        refreshmentsLogic.DeleteRefreshment(0);


        bool hasOldRefreshment = refreshments[0].Equals(oldDrink);

        Assert.AreEqual(false, hasOldRefreshment);
    }
}

