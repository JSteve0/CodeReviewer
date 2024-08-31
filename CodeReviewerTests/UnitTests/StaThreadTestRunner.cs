namespace CodeReviewerTests.UnitTests;

public class StaThreadTestRunner {

    public static void Run(Action action) {
        var thread = new Thread(() => { action(); });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join();
    }

}
