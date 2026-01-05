using chava.interfaceadapter;
using UnityEngine;
using Zenject;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace hector.view
{
    public class LogoView : MonoBehaviour
    {
        private LogoViewModel _viewModel;
        private List<IDisposable> _disposables = new();

        [SerializeField] private Button _nextButton;

        [Inject]
        private void Configure(LogoViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.NextButtonVisible.Subscribe(ShowHideNextButton).AddTo(_disposables);
            _nextButton.onClick.AddListener(() =>
            {
                //play animation
                //play sound
                _viewModel.PressNextButton.Execute(Unit.Default);
            });
            //  gameObject.SendMessage("TakeDamage", 5, SendMessageOptions.DontRequireReceiver);
        }

        private void ShowHideNextButton(bool isVisible)
        {
            if (_nextButton != null)
                _nextButton.gameObject.SetActive(isVisible);

        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            _disposables.Clear();
            _disposables = null;
        }
    }
}
