namespace CinemaApp;

[TestClass]
public class ReservationLogicTest
{
    private ReservationLogic _reservationLogic;
    private ReservationAccess _reservationAccess;

    [TestInitialize]
    public void Initialize()
    {
        _reservationLogic = new ReservationLogic();
        _reservationAccess = new ReservationAccess();
    }

    [TestMethod]
    public void TestGetReservations()
    {
        List<ReservationModel> reservations = _reservationLogic.GetReservations();
        Assert.IsNotNull(reservations);
    }

    [TestMethod]
    public void TestDeleteReservation()
    {
        List<ReservationModel> reservations = new List<ReservationModel>() { };

        ReservationModel reservation1 = new ReservationModel("id1", new List<SeatModel>(), 0, new List<RefreshmentModel>(), null, null);
        ReservationModel reservation2 = new ReservationModel("id2", new List<SeatModel>(), 0, new List<RefreshmentModel>(), null, null);
        reservations.Add(reservation1);
        reservations.Add(reservation2);
        _reservationLogic.UpdateReservations(reservations);

        _reservationLogic.DeleteReservation(reservation1, reservations);

        Assert.AreEqual(1, reservations.Count);
        Assert.AreEqual(reservation2, reservations[0]);
    }

    [TestMethod]
    public void TestCheckStringLengthForReceiptFormat()
    {
        string str = "test string";
        string label = "Label";

        string result = _reservationLogic.CheckStringLengthForReceiptFormatPublic(str, label);

        Assert.AreEqual("|  Label: test string                 |", result);
    }

    [TestMethod]
    public void TestRefreshmentsReceipt()
    {
        Dictionary<string, Dictionary<RefreshmentModel, int>> refreshments = new Dictionary<string, Dictionary<RefreshmentModel, int>>();
        RefreshmentModel popcorn = new RefreshmentModel("Popcorn", false, 5.99);
        RefreshmentModel soda = new RefreshmentModel("Soda", true, 2.99);
        refreshments.Add("Popcorn", new Dictionary<RefreshmentModel, int> { { popcorn, 2 } });
        refreshments.Add("Drinks", new Dictionary<RefreshmentModel, int> { { soda, 1 } });

        string result = _reservationLogic.RefreshmentsReceiptPublic(refreshments);

        Assert.AreEqual("|  Popcorn: 2 x 5.99           11.98  |\n|  Soda: 1 x 2.99           2.99      |", result);
    }

    [TestMethod]
    public void TestUpdateReservation()
    {
        List<ReservationModel> reservations = new List<ReservationModel>() { };

        ReservationModel reservation1 = new ReservationModel("id1", new List<SeatModel>(), 0, new List<RefreshmentModel>(), null, null);
        ReservationModel reservation2 = new ReservationModel("id2", new List<SeatModel>(), 0, new List<RefreshmentModel>(), null, null);
        reservations.Add(reservation1);
        reservations.Add(reservation2);
        _reservationLogic.UpdateReservations(reservations);

        _reservationLogic.DeleteReservation(reservation1, reservations);

        Assert.AreEqual(1, reservations.Count);
        Assert.AreEqual(reservation2, reservations[0]);
    }
}