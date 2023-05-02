using System;
using System.Collections.Generic;
using AxGrid.Base;
using Result.Task2.Code.Extensions;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Result.UniRxAndSmartformat.Code
{
    public class UniRxLoadImage : MonoBehaviourExt
    {
        [SerializeField] private TextMeshProUGUI tmp;
        [SerializeField] private Button loadButton;
        [SerializeField] private Image image;
        [SerializeField, TextArea(1, 2)] private List<string> textureURLs;

        private Texture2D currentTexture;
        private IDisposable requestDisposable;
        private IDisposable buttonDisposable;
        private Sprite defaultSprite;

        [OnStart]
        private void StartThis()
        {
            defaultSprite = image.sprite;
            buttonDisposable = loadButton.OnClickAsObservable().Subscribe(_ => LoadTexture());
        }

        [OnDestroy]
        private void DestroyThis()
        {
            buttonDisposable?.Dispose();
            Dispose();
        }

        private void LoadTexture()
        {
            Dispose();
            ResetImage();

            string url = textureURLs.GetRandomElement();

            requestDisposable = Load(url).Subscribe
            (
                result => SuccessLoad(result.webRequest),
                exception => FailLoad(exception.Message, url)
            );
        }

        private IObservable<UnityWebRequestAsyncOperation> Load(string url)
        {
            ScheduledNotifier<float> progressNotifier = Progress();

            return UnityWebRequestTexture
                .GetTexture(url)
                .SendWebRequest()
                .AsAsyncOperationObservable(progress: progressNotifier)
                .Select(result =>
                {
                    if (!string.IsNullOrEmpty(result.webRequest.error))
                        throw new Exception("invalid link");

                    return result;
                });
        }

        private ScheduledNotifier<float> Progress()
        {
            ScheduledNotifier<float> progressNotifier = new ScheduledNotifier<float>();
            progressNotifier.Subscribe(value => ChangeText($"Progress: {(int)(value * 100)}%"));

            return progressNotifier;
        }

        private void SuccessLoad(UnityWebRequest webRequest)
        {
            currentTexture = DownloadHandlerTexture.GetContent(webRequest);
            image.sprite = Sprite.Create(currentTexture, GetRect(), Vector2.one * .5f);
            ChangeText(string.Empty);
        }

        private Rect GetRect() =>
            new Rect(0, 0, currentTexture.width, currentTexture.height);

        private void FailLoad(string error, string url)
        {
            Dispose();
            ResetImage();
            ChangeText($"Error: <color=#FF8B8B>{error}</color><br>Url: <color=#FF8B8B>{url}</color>");
        }

        private void ChangeText(string text) =>
            tmp.text = text;

        private void ResetImage() =>
            image.sprite = defaultSprite;

        private void Dispose()
        {
            requestDisposable?.Dispose();

            if (currentTexture != null)
                Destroy(currentTexture);
        }
    }
}