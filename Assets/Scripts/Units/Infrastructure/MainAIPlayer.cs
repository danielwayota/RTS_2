public class MainAIPlayer : Base
{
    private int waveUnits = 128;
    private int currentUnits = 0;

    // =================================
    protected override void OnUpdate()
    {
        if (this.currentUnits < this.waveUnits)
        {
            this.CreateUnit("Trooper");
            this.currentUnits++;
        }
    }
}
