using R3;
namespace chava.interfaceadapter
{
    public class LogoViewModel : ViewModel
    {
        public ReactiveProperty<bool> NextButtonVisible = new(false);
        public ReactiveCommand PressNextButton = new();
    }
}
