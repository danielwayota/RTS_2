public class MainAIPlayer : Base
{
    public int waveUnits = 128;
    public int currentUnits = 0;

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
