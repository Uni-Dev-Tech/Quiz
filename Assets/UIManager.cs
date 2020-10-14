using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Массив загадок")] [SerializeField] private GameObject[] questions;
    [Header("Массив всех text отображающих номер вопроса")] [SerializeField] private Text[] questionNumbers;
    [Header("Массив всех text отображающих количество правильных ответов")] [SerializeField] private Text[] questionsCorrectAnswer;
    [Header("Первый и последний канвас")] [SerializeField] private GameObject startCanvas, endCanvas;
    [Header("Максимальное количество загаднных загадок")] [SerializeField] private byte maxQuastions;
    private GameObject currentScreen = null; // хранит отображающийся канвас
    private int textCount = 1; // счет уровней 
    private byte index = 0; // индекс для массива загадок
    private int correctAnswers = 0; // счет правильных  ответов
    [SerializeField] private InputField questionAboutHome, questionAboutRevenge, questionAboutMap, questionAboutTongue, questionAboutDreams; // все инпуты загадок
    /// <summary>
    /// Перемешивает загадки изменяя массив questions
    /// </summary>
    public void MixQuastions()
    {
        // Включает все канвасы загадок(нужно для сортировки в след цикле)
        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].SetActive(true);
        }
        //Перемешивает в случайном порядке все загадки(сортируя по состоянию активности объекта)
        GameObject[] questionsMixer = new GameObject[questions.Length];
        for(int i = 0; i < questionsMixer.Length; i++)
        {
            questionsMixer[i] = questions[Random.Range(0, 11)];
            if (!questionsMixer[i].activeSelf)
                i--;
            if (questionsMixer[i].activeSelf)
                questionsMixer[i].SetActive(false);
        }
        // Присваевает массиву questions новый порядок канвасов
        for(int i = 0; i < questionsMixer.Length; i++)
        {
            questions[i] = questionsMixer[i];
        }
    }
    /// <summary>
    /// Обеспечивает переход к новой загадке
    /// </summary>
    public void NextQuastion()
    {
        // При достижении максимального количество загаданных загадок выдает "завершающий" канвас с результатом
        if(index == maxQuastions)
        {
            currentScreen.SetActive(false);
            currentScreen = endCanvas;
            currentScreen.SetActive(true);
        }
        if(index < maxQuastions)
        {
            QuestionNumberChange();
            // используется при переходе с начального канваса
            if (currentScreen == null)
            {
                currentScreen = questions[index];
                startCanvas.SetActive(false);
                currentScreen.SetActive(true);
            }
            // обеспечивает переход с загадки на загадку
            if (currentScreen != null)
            {
                currentScreen.SetActive(false);
                currentScreen = questions[index];
                currentScreen.SetActive(true);
                index++;
            }
        }
    }
    /// <summary>
    /// Сбрасывает все значения к начальным(по-умолчанию) и переходит к первой загадке
    /// </summary>
    public void RepeatGame()
    {
        index = 0;
        correctAnswers = -1;
        QuestionCorrectAnswerChange();
        textCount = 1;
        QuestionNumberChange();
        MixQuastions();
        currentScreen = questions[index];
        endCanvas.SetActive(false);
        currentScreen.SetActive(true);
        questionAboutDreams.text = null;
        questionAboutHome.text = null;
        questionAboutRevenge.text = null;
        questionAboutMap.text = null;
        questionAboutTongue.text = null;
        index++;
    }
    /// <summary>
    /// Обновляет/изменяет текст отображающий номер загадки
    /// </summary>
    private void QuestionNumberChange()
    {
        for(int i = 0; i < questionNumbers.Length; i++)
        {
            questionNumbers[i].text = textCount.ToString();
        }
        textCount++;
    }
    /// <summary>
    /// Добавляет и обновляет текст отображающий количество отгаданых загадок 
    /// </summary>
    public void QuestionCorrectAnswerChange()
    {
        correctAnswers++;
        for (int i = 0; i < questionsCorrectAnswer.Length; i++)
        {
            questionsCorrectAnswer[i].text = correctAnswers.ToString();
        }
    }
    /// <summary>
    /// Проверяет правильность вводимых ответов
    /// </summary>
    /// <param name="currentInputField">Определяет к какой загадке относится инпут</param>
    public void InputAnswerNext(InputField currentInputField)
    {
        if (questionAboutHome == currentInputField)
        {
            if (questionAboutHome.text.ToLower() == "дом" )
                QuestionCorrectAnswerChange();
        }
        if (questionAboutMap == currentInputField)
        {
            if (questionAboutMap.text.ToLower() == "карта")
                QuestionCorrectAnswerChange();
        }
        if (questionAboutRevenge == currentInputField)
        {
            if (questionAboutRevenge.text.ToLower() == "месть")
                QuestionCorrectAnswerChange();
        }
        if (questionAboutTongue == currentInputField)
        {
            if (questionAboutTongue.text.ToLower() == "язык")
                QuestionCorrectAnswerChange();
        }
        if (questionAboutDreams == currentInputField)
        {
            if (questionAboutDreams.text.ToLower() == "сны" || questionAboutDreams.text.ToLower() == "сон")
                QuestionCorrectAnswerChange();
        }
    }
    /// <summary>
    /// Переход в главное меню
    /// </summary>
    public void ToMainMenu()
    {
        index = 0;
        correctAnswers = -1;
        QuestionCorrectAnswerChange();
        textCount = 0;
        QuestionNumberChange();
        currentScreen = null;
        endCanvas.SetActive(false);
        startCanvas.SetActive(true);
        questionAboutDreams.text = null;
        questionAboutHome.text = null;
        questionAboutRevenge.text = null;
        questionAboutMap.text = null;
        questionAboutTongue.text = null;
    }
}
