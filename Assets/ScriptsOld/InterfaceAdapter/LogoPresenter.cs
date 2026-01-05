using chava.domain;
using System;
using R3;
using System.Collections.Generic;

namespace chava.interfaceadapter
{
    public class LogoPresenter : IDisposable, IShowView, IHideView
    {
        private readonly LogoViewModel _viewModel = null;
        private readonly ISceneLoad _sceneLoad = null;
        private List<IDisposable> _disposables = new();

        public LogoPresenter(LogoViewModel viewModel, ISceneLoad sceneLoad)
        {
            _viewModel = viewModel;
            _sceneLoad = sceneLoad;

            _viewModel.PressNextButton.Subscribe(_ =>
            {
                _sceneLoad.ActivateScene();
            }).AddTo(_disposables);
        }

        public void Dispose() // OnDestroy
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            _disposables.Clear();
        }

        public void Show()
        {
            _viewModel.NextButtonVisible.Value = true;
            // _viewModel.Visible.Value = true; para la ventana completa
        }

        public void Hide()
        {
            _viewModel.NextButtonVisible.Value = false;
            // _viewModel.Visible.Value = false; para la ventana completa
        }
    }
}