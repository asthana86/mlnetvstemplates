﻿/* This template shows how to use a model trained with ML.NET (https://aka.ms/mlnet) to make predictions.
 * This model predicts whether a sentence has a positive or negative sentiment. It is based on a sample that can be 
 * found at https://aka.ms/mlnetsentimentanalysis, which provides a more detailed introduction to ML.NET and the scenario.
 * Note that this model was trained on a very small sample dataset, which leads to a relatively low accuracy.
 */
using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Runtime.Api;

namespace MLNETModelLibrary
{
    public static class ModelPrediction
    {
        /*
         * e.g. MLNETModelLibrary.ModelPrediction.Predict(new MLNETModelLibrary.SentimentData {SentimentText = "ML.NET is fun, more samples at https://github.com/dotnet/machinelearning-samples"}); 
         */
        public static string Predict(SentimentData input)
        {
              // 1. Load the model from file.
              var model = PredictionModel.ReadAsync<SentimentData, SentimentPrediction>(@"Models\sentiment_model.zip").Result;

              // 2. Use the model for a single prediction.
              var sentiment = (model.Predict(input).Sentiment == true) ? "Positive" : "Negative";
              Console.WriteLine("Predicted sentiment for \"" + input.SentimentText + "\" is:" + sentiment);
              return sentiment;
         }
    }

    // Input class that tells ML.NET how to read the input for predictions.
    public class SentimentData
    {
        [Column(ordinal: "0", name: "Label")]
        public float Sentiment;
        [Column(ordinal: "1")]
        public string SentimentText;
    }

    // Output class for the prediction, in this case including only the predicted sentiment.
    public class SentimentPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Sentiment;
    }

}
