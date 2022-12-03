using Avalonia.Controls;
using MessageBox.Avalonia;
using System.Linq;
using System;
using ������;
using System.Collections.Generic;
using System.Text;
using UI.Models;
using Avalonia.Interactivity;

namespace UI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object? sender, RoutedEventArgs e)
        {
            if (!CheckFields())
                return;

            treeView1.Items = null;
            TablesHolder.Tables.Clear();
            AlphabetHolder.InitAlphabet(tbComments.Text, tbAlphabet.Text);
            var analyzer = GetAnalyzerByChoosedLab();

            try
            {
                if (cbLabNum.SelectedIndex < 3)
                    tbMessages.Text = string.Join(", ", analyzer.Analysis(tbText.Text)) + $"{Environment.NewLine}����� �����";
                else if (cbLabNum.SelectedIndex == 3)
                {
                    analyzer.Analysis(tbText.Text);
                    PrintHashTables();
                }
                else if (cbLabNum.SelectedIndex == 4 || cbLabNum.SelectedIndex == 5)
                {
                    analyzer.Analysis(tbText.Text);
                    PrintHashTables();
                    PrintTreeView();
                }
                else if (cbLabNum.SelectedIndex == 6)
                {
                    analyzer.Analysis(tbText.Text);
                    PrintTreeView();
                    ICodeGenerator generator = new AssamblerCodeGenerator();
                    tbMessages.Text = generator.Generate(TablesHolder.Tables.Values.SelectMany(x => x).ToList());
                }
            }
            catch (Exception ex)
            {
                MessageBoxManager.GetMessageBoxStandardWindow("������", ex.Message, 
                    MessageBox.Avalonia.Enums.ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Error).Show();
            }
        }

        private void PrintHashTables()
        {
            StringBuilder result = new StringBuilder();

            var isAnyData = TablesHolder.Tables.TryGetValue(1, out var values);
            if (isAnyData)
                result.Append($"���� ���� ������� ����: {string.Join(", ", values.Select(x => x.Hash))}{Environment.NewLine}");

            isAnyData = TablesHolder.Tables.TryGetValue(2, out values);
            if (isAnyData)
                result.Append($"���� ���� ������� ����: {string.Join(", ", values.Select(x => x.Hash))}{Environment.NewLine}");

            isAnyData = TablesHolder.Tables.TryGetValue(0, out values);
            if (isAnyData)
                result.Append($"���� ��������� ����: {string.Join(", ", values.Select(x => x.Hash))}");

            tbMessages.Text = result.ToString();
        }

        private void PrintTreeView()
        {
            var words = TablesHolder.Tables.Values.SelectMany(x => x).ToList();

            var collection = new List<TreeViewNode>();
            var rootElement = new TreeViewNode("�����");

            // ��������� ���� ��� ����� ����������� "adcbbc := [ 000101110 , 000101110 , ... ] ;"
            TablesHolder.Tables.TryGetValue(0, out var specialSymbols);
            var countOfSentences = specialSymbols.Count(x => x.Context.Value == ContextValues.DotAndComma) + 1;
            for (int i = 0; i < countOfSentences; i++)
                rootElement.Childrens.Add(new TreeViewNode($"����������� {i + 1}"));

            // ��������� �������� ������ �����
            for (int i = 0; i < countOfSentences; i++)
            {
                // ����� ���������� 7, �� �� �� ��������� ";", ������� ���� �� 6
                // j - ������� ����� ���������
                for (int j = 0; j < 6; j++)
                {
                    // ����� � ���������� ���������� - ��� ����� ���� "000101110"
                    var similarContextItems = words.Where(x => x.Context.SentenceNumber == i && x.Context.Value == (ContextValues)j);
                    foreach (var item in similarContextItems)
                        rootElement.Childrens[i].Childrens.Add(new TreeViewNode(item.Value));
                }
            }

            collection.Add(rootElement);
            treeView1.Items = collection;
        }

        /// <summary>
        /// �������� ������ ���������� ����������� � ����������� �� ������� ����
        /// </summary>
        private BaseLexicalAnalyzer GetAnalyzerByChoosedLab()
        {
            var analyzer = new BaseLexicalAnalyzer();
            switch (cbLabNum.SelectedIndex)
            {
                case 0: analyzer = new BaseLexicalAnalyzer(); break;
                case 1: analyzer = new LexicalAnalyzerLab2(); break;
                case 2:
                case 3:
                case 4: analyzer = new LexicalAnalyzerLab3(); break;
                case 5:
                case 6: analyzer = new LexicalAnalyzerLab6(); break;
            }
            return analyzer;
        }

        /// <summary>
        /// �������� �������� ������ ����� ��������
        /// </summary>
        /// <returns>true - ���� �� �����, ����� - false</returns>
        /// <remarks>�������������� ������� <see cref="MessageBox"/> � ��������� ������</remarks>
        private bool CheckFields()
        {
            var fields = new List<TextBox>() { tbAlphabet, tbText, tbComments };
            foreach (var field in fields)
            {
                if (string.IsNullOrEmpty(field.Text) || string.IsNullOrWhiteSpace(field.Text))
                {
                    MessageBoxManager.GetMessageBoxStandardWindow("������", "���� ��� ��������� ����� �� ���������. ��������� ��.",
                        MessageBox.Avalonia.Enums.ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Error).Show();
                    return false;
                }
            }

            if (tbComments.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length != 2)
            {
                MessageBoxManager.GetMessageBoxStandardWindow("������", "����������� ������ �������� �� ���� ����",
                        MessageBox.Avalonia.Enums.ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Error).Show();
                return false;
            }

            return true;
        }

    }
}