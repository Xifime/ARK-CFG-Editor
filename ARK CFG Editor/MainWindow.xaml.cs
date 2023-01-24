using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Configuration;
using System.Collections.Specialized;
using System.Net;
using System.Diagnostics;
using System.Reflection;

namespace ARK_CFG_Editor
{
    public partial class MainWindow : Window
    {
        #region VariablesWarmup
        public bool CShadows;

        public bool CEffects;

        public bool CEmitters;

        public bool CFogHEAVY;

        public bool CFogLIGHT;

        public bool CFoliage;

        public bool CWaterSurface;

        public bool CLimitSettings;

        public bool CNoCAP;

        public bool COptimization;

        public bool Warnings = true;

        public bool free = true;

        public string path = ConfigurationManager.AppSettings["ARKPath"].ToString();
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            #region Path
            if (path.Length < 1)
            {
                foreach(DriveInfo drive in DriveInfo.GetDrives())
                {
                    string cdrive = drive.ToString().Replace("\\", "/");
                    string pat = "Steam/steamapps/common/ARK/Engine/Config";
                    string pat2 = "SteamLibrary/steamapps/common/ARK/Engine/Config";
                    if (File.Exists(cdrive + pat + "/BaseEngine.ini"))
                    {
                        FillFile(cdrive + pat);
                    }
                    if (File.Exists(cdrive + pat2 + "/BaseEngine.ini"))
                    {
                        FillFile(cdrive + pat2);
                    }
                }
                if(path.Length < 1)
                {
                    if (File.Exists("C:/Program Files/Steam/steamapps/common/ARK/Engine/Config/BaseEngine.ini"))
                    {
                        FillFile("C:/Program Files/Steam/steamapps/common/ARK/Engine/Config");
                    }
                    if (File.Exists("C:/Program Files (x86)/Steam/steamapps/common/ARK/Engine/Config/BaseEngine.ini"))
                    {
                        FillFile("C:/Program Files (x86)/Steam/steamapps/common/ARK/Engine/Config");
                    }
                    if (File.Exists("C:/Program Files/SteamLibrary/steamapps/common/ARK/Engine/Config/BaseEngine.ini"))
                    {
                        FillFile("C:/Program Files/SteamLibrary/steamapps/common/ARK/Engine/Config");
                    }
                    if (File.Exists("C:/Program Files (x86)/SteamLibrary/steamapps/common/ARK/Engine/Config/BaseEngine.ini"))
                    {
                        FillFile("C:/Program Files (x86)/SteamLibrary/steamapps/common/ARK/Engine/Config");
                    }
                }
            } // Automatic directory search (Almost perfect)
            if (path.Length < 1)
            {
                MessageBox.Show("Обнаружен первый запуск программы! Вам нужно указать путь к Диск:/БиблиотекаСтим/SteamApps/ARK/Engine/Config", "ARK CFG Editor", MessageBoxButton.OK, MessageBoxImage.Information);
                var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = cfg.AppSettings.Settings;
                System.Windows.Forms.FolderBrowserDialog opfn = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult res = opfn.ShowDialog();
                if (res == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(opfn.SelectedPath) && opfn.SelectedPath.Replace("\\", "/").Contains("Engine/Config"))
                {
                    settings.Remove("ARKPath");
                    settings.Add("ARKPath", opfn.SelectedPath.Replace("\\", "/"));
                    settings.Remove("Erased");
                    settings.Remove("Presets");
                    cfg.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(cfg.AppSettings.SectionInformation.Name);
                    path = opfn.SelectedPath.Replace("\\", "/");
                    Path.Text = opfn.SelectedPath.Replace("\\", "/");
                    MessageBox.Show("Путь к изменяемым файлам успешно указан!", "ARK CFG Editor", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Путь скорее всего, не ведёт к файлам конфигурации. Попробуйте ещё раз, или посмотрите видео-инструкцию!", "ARK CFG Editor", MessageBoxButton.OK, MessageBoxImage.Error);
                    Environment.Exit(1);
                }
            } 
            #endregion

            #region Startup
            MessageBox.Show("Мы не несём ответственности за ваш аккаунт! Используйте программу на свой страх и риск. Согласно ToS игры изменение игрового движка запрещено.", "ARK CFG Editor", MessageBoxButton.OK, MessageBoxImage.Warning);
            KeyCode.GotFocus += RemoveText;
            KeyCode.LostFocus += AddText;
            Path.GotFocus += RemoveText2;
            Path.LostFocus += AddText2;
            ConnectBug.GotFocus += RemoveText3;
            ConnectBug.LostFocus += AddText3;
            KeyCode.Text = "Введите ключ...";
            ConnectBug.Text = "Укажите IP сервера!";
            CBShadows.Visibility = Visibility.Hidden;
            CBEffects.Visibility = Visibility.Hidden;
            CBEmitters.Visibility = Visibility.Hidden;
            CBFogHEAVY.Visibility = Visibility.Hidden;
            CBFogLIGHT.Visibility = Visibility.Hidden;
            CBFoliage.Visibility = Visibility.Hidden;
            Path.Visibility = Visibility.Hidden;
            ConnectBug.Visibility = Visibility.Hidden;
            ConnectBug2.Visibility = Visibility.Hidden;
            ConnectBug3.Visibility = Visibility.Hidden;
            CBWaterSurface.Visibility = Visibility.Hidden;
            CBOptimization.Visibility = Visibility.Hidden;
            CBFPSCap.Visibility = Visibility.Hidden;
            CBLimitSettings.Visibility = Visibility.Hidden;
            CopyBtn.Visibility = Visibility.Hidden;
            if (free)
            {
                KeyCode.Visibility = Visibility.Hidden;
                LoadBtn.Visibility = Visibility.Hidden;
                CopyBtn.Visibility = Visibility.Visible;
                CBShadows.Visibility = Visibility.Visible;
                CBEffects.Visibility = Visibility.Visible;
                CBEmitters.Visibility = Visibility.Visible;
                CBFogHEAVY.Visibility = Visibility.Visible;
                CBFogLIGHT.Visibility = Visibility.Visible;
                CBFoliage.Visibility = Visibility.Visible;
                CBWaterSurface.Visibility = Visibility.Visible;
                CBOptimization.Visibility = Visibility.Visible;
                CBFPSCap.Visibility = Visibility.Visible;
                CBLimitSettings.Visibility = Visibility.Visible;
                BuildBtn.Visibility = Visibility.Visible;
                ConnectBug.Visibility = Visibility.Visible;
                ConnectBug2.Visibility = Visibility.Visible;
                ConnectBug3.Visibility = Visibility.Visible;
                if(ConfigurationManager.AppSettings["Presets"] != null)
                {
                    var paths = new List<string>(ConfigurationManager.AppSettings["Presets"].Split(new char[] { ',' }));
                    foreach(string path in paths)
                    {
                        Presets.Items.Add(path);
                    }
                }
                if (ConfigurationManager.AppSettings["Erased"] != null)
                {
                    var paths = new List<string>(ConfigurationManager.AppSettings["Erased"].Split(new char[] { ',' }));
                    foreach (string path in paths)
                    {
                        Presets.Items.Remove(path);
                    }
                }
                if (path.Length < 2)
                {
                    Path.Text = "Укажите путь к ARK!";
                }
                else
                {
                    Path.Text = path;
                }
            }
            #endregion
        }

        #region Functions
        public static string ShowDialog(string text, string caption)
        {
            System.Windows.Forms.Form prompt = new System.Windows.Forms.Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            };
            System.Windows.Forms.Label textLabel = new System.Windows.Forms.Label() { Left = 50, Top = 20, Text = text, Width = 200 };
            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox() { Left = 50, Top = 50, Width = 400 };
            System.Windows.Forms.Button confirmation = new System.Windows.Forms.Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = System.Windows.Forms.DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == System.Windows.Forms.DialogResult.OK ? textBox.Text : "";
        }
        public void FillFile(string pat)
        {
            MessageBoxResult result = MessageBox.Show("Программа автоматически обнаружила директорию ARK! Пожалуйста, сверьте информацию. Путь: " + pat, "ARK CFG Editor", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = cfg.AppSettings.Settings;
                settings.Remove("ARKPath");
                settings.Add("ARKPath", pat);
                settings.Remove("Erased");
                settings.Remove("Presets");
                cfg.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(cfg.AppSettings.SectionInformation.Name);
                path = pat;
                Path.Text = pat;
            }
        }
        #endregion

        #region Design
        public void RemoveText(object sender, EventArgs e)
        {
            if (KeyCode.Text == "Введите ключ...")
            {
                KeyCode.Text = "";
            }
        }

        public void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(KeyCode.Text))
                KeyCode.Text = "Введите ключ...";
        }

        public void RemoveText2(object sender, EventArgs e)
        {
            if (Path.Text == "Укажите путь к ARK!")
            {
                Path.Text = "";
            }
        }

        public void AddText3(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ConnectBug.Text))
                ConnectBug.Text = "Укажите IP сервера!";
        }

        public void RemoveText3(object sender, EventArgs e)
        {
            if (ConnectBug.Text == "Укажите IP сервера!")
            {
                ConnectBug.Text = "";
            }
        }

        public void AddText2(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Path.Text))
                Path.Text = "Укажите путь к ARK!";
        }
        #endregion

        #region Buttons
        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            string text = KeyCode.Text;
            if (text == "123")
            {
                KeyCode.Visibility = Visibility.Hidden;
                LoadBtn.Visibility = Visibility.Hidden;
                CBShadows.Visibility = Visibility.Visible;
                CBEffects.Visibility = Visibility.Visible;
                CBEmitters.Visibility = Visibility.Visible;
                CBFogHEAVY.Visibility = Visibility.Visible;
                CBFogLIGHT.Visibility = Visibility.Visible;
                CBFoliage.Visibility = Visibility.Visible;
                CBWaterSurface.Visibility = Visibility.Visible;
                CBOptimization.Visibility = Visibility.Visible;
                CBFPSCap.Visibility = Visibility.Visible;
                CBLimitSettings.Visibility = Visibility.Visible;
                BuildBtn.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("False");
            }
        }
        private void NewPath_Click(object sender, RoutedEventArgs e)
        {
            var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = cfg.AppSettings.Settings;
            System.Windows.Forms.FolderBrowserDialog opfn = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult res = opfn.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(opfn.SelectedPath) && opfn.SelectedPath.Replace("\\", "/").Contains("Engine/Config"))
            {
                settings.Remove("ARKPath");
                settings.Add("ARKPath", opfn.SelectedPath.Replace("\\", "/"));
                cfg.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(cfg.AppSettings.SectionInformation.Name);
                MessageBox.Show("Путь к изменяемым файлам успешно указан!", "ARK CFG Editor", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Путь скорее всего, не ведёт к файлам конфигурации. Попробуйте ещё раз, или посмотрите видео-инструкцию!", "ARK CFG Editor", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void UnlockMinimaps_Click(object sender, RoutedEventArgs e)
        {
            if (Path.Text.Contains("Engine/Config"))
            {
                string paty = @Path.Text;
                string newPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(paty, @"..\..\ShooterGame/Saved/LocalProfiles/PlayerLocalData.arkprofile"));
                if (File.Exists(newPath))
                {
                    MessageBoxResult res = MessageBox.Show("Функция ещё тестируется! Использовать на свой страх и риск! [ВОЗМОЖНА ПОТЕРЯ ДАННЫХ НЕКОТОРЫХ КАРТ]", "ARK CFG Editor", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if(res == MessageBoxResult.Yes)
                    {
                        WebClient webClient = new WebClient();
                        try
                        {
                            webClient.DownloadFile("https://drive.google.com/u/0/uc?id=1cc2CqR3MG9NkDe8KwbLgSveJUdmGdbqi&export=download", "PlayerLocalData.arkprofile");
                        }
                        catch
                        {
                            MessageBox.Show("Невозможно установить добавляемый файл!", "ARK CFG Editor", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        File.Delete(newPath);
                        File.Copy("PlayerLocalData.arkprofile", newPath);
                        File.Delete("PlayerLocalData.arkprofile");
                        MessageBox.Show("Все карты разблокированы. (Если вы были в игре то действие не сработает)", "ARK CFG Editor", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Получена критическая ошибка! Действие невозможно. Файл несуществует или путь указан неправильно.", "ARK CFG Editor", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #region Build&Copy
        private void BuildBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!File.Exists(Path.Text + "/BaseDeviceProfiles.ini"))
                {
                    if (Path.Text.Contains("Engine/Config"))
                    {
                        File.Create(Path.Text + "/BaseDeviceProfiles.ini").Close();
                    }
                    else
                    {
                        MessageBox.Show("Укажите действительный путь к файлу BaseDeviceProfiles.ini (Можно использовать конфигурацию)!", "ARK CFG EDITOR", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                string[] casesr = File.ReadAllLines("copydata/Reader.ini");
                File.WriteAllText(Path.Text + "/BaseDeviceProfiles.ini", "");
                foreach (string line in casesr)
                {
                    File.AppendAllText(Path.Text + "/BaseDeviceProfiles.ini", Environment.NewLine + line);
                }
                File.AppendAllText(Path.Text + "/BaseDeviceProfiles.ini", "; /// Сгенерировано в ARK CFG EDITOR ///" + Environment.NewLine + Environment.NewLine + "[Windows DeviceProfile]" + Environment.NewLine);
                if (CNoCAP)
                {
                    File.AppendAllText(Path.Text + "/BaseDeviceProfiles.ini", Environment.NewLine +
                        "+CVars=t.maxfps=360");
                }
                if (COptimization)
                {
                    File.AppendAllText(Path.Text + "/BaseDeviceProfiles.ini", Environment.NewLine +
                        "+CVars=FrameRateCap=144" + Environment.NewLine +
                        "+CVars=FrameRateMinimum=144" + Environment.NewLine +
                        "+CVars=MaxES2PixelShaderAdditiveComplexityCount=45" + Environment.NewLine +
                        "+CVars=MaxPixelShaderAdditiveComplexityCount=128" + Environment.NewLine +
                        "+CVars=MaxSmoothedFrameRate=144" + Environment.NewLine +
                        "+CVars=MinDesiredFrameRate=70" + Environment.NewLine +
                        "+CVars=MinSmoothedFrameRate=5");
                }
                if (CShadows)
                {
                    File.AppendAllText(Path.Text + "/BaseDeviceProfiles.ini", Environment.NewLine +
                        "+CVars=ShowFlag.LightComplexity=0" + Environment.NewLine +
                        "+CVars=ShowFlag.LightShafts=0" + Environment.NewLine +
                        "+CVars=ShowFlag.ShaderComplexity=0" + Environment.NewLine +
                        "+CVars=ShowFlag.Refraction=0");
                }
                if (CEffects)
                {
                    File.AppendAllText(Path.Text + "/BaseDeviceProfiles.ini", Environment.NewLine +
                        "+CVars=ShowFlag.Materials=0" + Environment.NewLine +
                        "+CVars=r.CustomDepth=0");
                }
                if (CEmitters)
                {
                    File.AppendAllText(Path.Text + "/BaseDeviceProfiles.ini", Environment.NewLine +
                        "+CVars=FX.MaxCPUParticlesPerEmitter=1");
                }
                if (CFogHEAVY)
                {
                    File.AppendAllText(Path.Text + "/BaseDeviceProfiles.ini", Environment.NewLine +
                        "+CVars=ShowFlag.Splines=0");
                }
                if (CFogLIGHT)
                {
                    File.AppendAllText(Path.Text + "/BaseDeviceProfiles.ini", Environment.NewLine +
                        "+CVars=ShowFlag.Fog=0");
                }
                if (CFoliage)
                {
                    File.AppendAllText(Path.Text + "/BaseDeviceProfiles.ini", Environment.NewLine +
                        "+CVars=foliage.UseOcclusionType=0");
                }
                if (CWaterSurface)
                {
                    File.AppendAllText(Path.Text + "/BaseDeviceProfiles.ini", Environment.NewLine +
                        "+CVars=ShowFlag.GameplayDebug=0");
                }
                if (CLimitSettings)
                {
                    string[] cases = File.ReadAllLines("copydata/LimitSettings.txt");
                    foreach (string line in cases)
                    {
                        File.AppendAllText(Path.Text + "/BaseDeviceProfiles.ini", Environment.NewLine + line);
                    }
                }
                MessageBox.Show("Установка прошла успешно!", "ARK CFG EDITOR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void CopyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists("copydata/BaseDeviceProfiles.ini"))
            {
                File.Create("copydata/BaseDeviceProfiles.ini").Close();
            }
            string[] casesr = File.ReadAllLines("copydata/Reader.ini");
            File.WriteAllText(Path.Text + "/BaseDeviceProfiles.ini", "");
            foreach (string line in casesr)
            {
                File.AppendAllText(Path.Text + "/BaseDeviceProfiles.ini", Environment.NewLine + line);
            }
            File.AppendAllText("copydata/BaseDeviceProfiles.ini", "; /// Сгенерировано в ARK CFG EDITOR ///" + Environment.NewLine + Environment.NewLine + "[Startup]" + Environment.NewLine);
            if (CNoCAP)
            {
                File.AppendAllText("copydata/BaseDeviceProfiles.ini", Environment.NewLine +
                    "t.maxfps=360");
            }
            if (COptimization)
            {
                File.AppendAllText("copydata/BaseDeviceProfiles.ini", Environment.NewLine +
                    "FrameRateCap=144" + Environment.NewLine +
                    "FrameRateMinimum=144" + Environment.NewLine +
                    "MaxES2PixelShaderAdditiveComplexityCount=45" + Environment.NewLine +
                    "MaxPixelShaderAdditiveComplexityCount=128" + Environment.NewLine +
                    "MaxSmoothedFrameRate=144" + Environment.NewLine +
                    "MinDesiredFrameRate=70" + Environment.NewLine +
                    "MinSmoothedFrameRate=5");
            }
            if (CShadows)
            {
                File.AppendAllText("copydata/BaseDeviceProfiles.ini", Environment.NewLine +
                    "ShowFlag.LightComplexity=0" + Environment.NewLine +
                    "ShowFlag.LightShafts=0" + Environment.NewLine +
                    "ShowFlag.ShaderComplexity=0" + Environment.NewLine +
                    "ShowFlag.Refraction=0");
            }
            if (CEffects)
            {
                File.AppendAllText("copydata/BaseDeviceProfiles.ini", Environment.NewLine +
                    "ShowFlag.Materials=0" + Environment.NewLine +
                    "r.CustomDepth=0");
            }
            if (CEmitters)
            {
                File.AppendAllText("copydata/BaseDeviceProfiles.ini", Environment.NewLine +
                    "FX.MaxCPUParticlesPerEmitter=1");
            }
            if (CFogHEAVY)
            {
                File.AppendAllText("copydata/BaseDeviceProfiles.ini", Environment.NewLine +
                    "ShowFlag.Splines=0");
            }
            if (CFogLIGHT)
            {
                File.AppendAllText("copydata/BaseDeviceProfiles.ini", Environment.NewLine +
                    "ShowFlag.Fog=0");
            }
            if (CFoliage)
            {
                File.AppendAllText("copydata/BaseDeviceProfiles.ini", Environment.NewLine +
                    "foliage.UseOcclusionType=0");
            }
            if (CWaterSurface)
            {
                File.AppendAllText("copydata/BaseDeviceProfiles.ini", Environment.NewLine +
                    "ShowFlag.GameplayDebug=0");
            }
            if (CLimitSettings)
            {
                string[] cases = File.ReadAllLines("copydata/LimitSettings.txt");
                foreach (string line in cases)
                {
                    File.AppendAllText("copydata/BaseDeviceProfiles.ini", Environment.NewLine + line);
                }
            }
            StringCollection paths = new StringCollection();
            paths.Add(Environment.CurrentDirectory + "/copydata/BaseDeviceProfiles.ini");
            Clipboard.Clear();
            Clipboard.SetFileDropList(paths);
            MessageBox.Show("Файл скопирован в буфер обмена!", "ARK CFG EDITOR", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region Connect
        private void ConnectBug2_Click(object sender, RoutedEventArgs e)
        {
            if (!ConnectBug.Text.Contains(":"))
            {
                return;
            }
            if (!ConnectBug.Text.Contains("."))
            {
                return;
            }
            if (ConnectBug.Text.Length < 5)
            {
                return;
            }
            System.Diagnostics.Process.Start("steam://connect/" + ConnectBug.Text);
        }

        private void ConnectBug3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Укажите IP сервера для подключения. Метод используется для быстрого подключения с сервера на сервер.", "Magic Transfer", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
        #region Presets
        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = cfg.AppSettings.Settings;
            string result = ShowDialog("Назовите ваш пресет!", "ARK CFG Editor");
            if(result.Contains(";") || result.Contains(",") || result.Contains(" ") || result.Contains("&") || result.Contains("*") || result.Contains("?") || result.Contains("/") || result.Contains("\\") || result.Contains("@") || result.Contains("#") || result.Contains("!") || result.Contains("$") || result.Contains("%") || result.Contains("^") || result.Contains(":") || result.Contains("(") || result.Contains(")") || result.Contains("-") || result.Contains("+") || result.Contains("<") || result.Contains(">") || result.Contains("."))
            {
                MessageBox.Show("Результат содержит юникод. Невозможно назвать пресет.", "ARK CFG Editor", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!File.Exists("copydata/" + result + ".ini"))
            {
                File.Create("copydata/" + result + ".ini").Close();
                settings.Add("Presets", result);
                cfg.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(cfg.AppSettings.SectionInformation.Name);
                Presets.Items.Add(result);
            }
            File.WriteAllText("copydata/" + result + ".ini", "; /// Сгенерировано в ARK CFG EDITOR ///" + Environment.NewLine + Environment.NewLine + "[Startup]" + Environment.NewLine);
            if (CNoCAP)
            {
                File.AppendAllText("copydata/" + result + ".ini", Environment.NewLine +
                    "t.maxfps=360");
            }
            if (COptimization)
            {
                File.AppendAllText("copydata/" + result + ".ini", Environment.NewLine +
                    "FrameRateCap=144" + Environment.NewLine +
                    "FrameRateMinimum=144" + Environment.NewLine +
                    "MaxES2PixelShaderAdditiveComplexityCount=45" + Environment.NewLine +
                    "MaxPixelShaderAdditiveComplexityCount=128" + Environment.NewLine +
                    "MaxSmoothedFrameRate=144" + Environment.NewLine +
                    "MinDesiredFrameRate=70" + Environment.NewLine +
                    "MinSmoothedFrameRate=5");
            }
            if (CShadows)
            {
                File.AppendAllText("copydata/" + result + ".ini", Environment.NewLine +
                    "ShowFlag.LightComplexity=0" + Environment.NewLine +
                    "ShowFlag.LightShafts=0" + Environment.NewLine +
                    "ShowFlag.ShaderComplexity=0" + Environment.NewLine +
                    "ShowFlag.Refraction=0");
            }
            if (CEffects)
            {
                File.AppendAllText("copydata/" + result + ".ini", Environment.NewLine +
                    "ShowFlag.Materials=0" + Environment.NewLine +
                    "r.CustomDepth=0");
            }
            if (CEmitters)
            {
                File.AppendAllText("copydata/" + result + ".ini", Environment.NewLine +
                    "FX.MaxCPUParticlesPerEmitter=1");
            }
            if (CFogHEAVY)
            {
                File.AppendAllText("copydata/" + result + ".ini", Environment.NewLine +
                    "ShowFlag.Splines=0");
            }
            if (CFogLIGHT)
            {
                File.AppendAllText("copydata/" + result + ".ini", Environment.NewLine +
                    "ShowFlag.Fog=0");
            }
            if (CFoliage)
            {
                File.AppendAllText("copydata/" + result + ".ini", Environment.NewLine +
                    "foliage.UseOcclusionType=0");
            }
            if (CWaterSurface)
            {
                File.AppendAllText("copydata/" + result + ".ini", Environment.NewLine +
                    "ShowFlag.GameplayDebug=0");
            }
            if (CLimitSettings)
            {
                string[] cases = File.ReadAllLines("copydata/LimitSettings.txt");
                foreach (string line in cases)
                {
                    File.AppendAllText("copydata/" + result + ".ini", Environment.NewLine + line);
                }
            }
        }
        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            if(Presets.SelectedItem == null)
            {
                MessageBox.Show("Укажите удаляемый объект!", "ARK CFG Editor", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = cfg.AppSettings.Settings;
            settings.Add("Erased", Presets.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", ""));
            cfg.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(cfg.AppSettings.SectionInformation.Name);
            File.Delete("copydata/" + Presets.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", "") + ".ini");
            Presets.Items.Remove(Presets.SelectedItem);
        }
        private void InstallBtn_Click(object sender, RoutedEventArgs e)
        {
            if(File.Exists("copydata/" + Presets.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", "") + ".ini") && File.Exists(Path.Text + "/BaseDeviceProfiles.ini"))
            {
                File.Delete(Path.Text + "/BaseDeviceProfiles.ini");
                string[] m = File.ReadAllLines("copydata/" + Presets.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", "") + ".ini");
                string[] casesr = File.ReadAllLines("copydata/Reader.ini");
                File.WriteAllText(Path.Text + "/BaseDeviceProfiles.ini", "");
                foreach (string line in casesr)
                {
                    File.AppendAllText(Path.Text + "/BaseDeviceProfiles.ini", Environment.NewLine + line);
                }
                File.AppendAllText(Path.Text + "/BaseDeviceProfiles.ini", "; /// Сгенерировано в ARK CFG EDITOR ///" + Environment.NewLine + Environment.NewLine + "[Windows DeviceProfile]" + Environment.NewLine);
                foreach (string mp in m)
                {
                    if(mp == "; /// Сгенерировано в ARK CFG EDITOR ///")
                    {

                    }
                    else
                    {
                        File.AppendAllText(Path.Text + "/BaseDeviceProfiles.ini", Environment.NewLine + "+CVars=" + mp);
                    }
                }
                MessageBox.Show("Конфигурация успешно установлена!", "ARK CFG Editor", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Пресет не существует системно!", "ARK CFG Editor", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
        #endregion

        #region Checks
        private void CBShadows_Check(object sender, RoutedEventArgs e)
        {
            if (CBShadows.IsChecked == true)
            {
                CShadows = true;
            }
            else
            {
                CShadows = false;
            }
        }

        private void CBEffects_Check(object sender, RoutedEventArgs e)
        {
            if (CBEffects.IsChecked == true)
            {
                CEffects = true;
                if (Warnings)
                {
                    MessageBox.Show("Тек и рельсотрон будут полностью отключены!", "ARK CFG EDITOR", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                CEffects = false;
            }
        }

        private void CBEmitters_Check(object sender, RoutedEventArgs e)
        {
            if (CBEmitters.IsChecked == true)
            {
                CEmitters = true;
                if (Warnings)
                {
                    MessageBox.Show("Видимость плевка ядовитой виверны и василиска будут ограничены!", "ARK CFG EDITOR", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                CEmitters = false;
            }
        }

        private void CBFogHEAVY_Check(object sender, RoutedEventArgs e)
        {
            if (CBFogHEAVY.IsChecked == true)
            {
                CFogHEAVY = true;
                if (Warnings)
                {
                    MessageBox.Show("Половина эффектов для комфортной игры будут полностью отключены, сюда входят чертежи построек, стрелы и другие вещи!", "ARK CFG EDITOR", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
            }
            else
            {
                CFogHEAVY = false;
            }
        }

        private void CBFogLIGHT_Check(object sender, RoutedEventArgs e)
        {
            if (CBFogLIGHT.IsChecked == true)
            {
                CFogLIGHT = true;
                if (Warnings)
                {
                    MessageBox.Show("Текст выделенного хранилища будет отображаться некорректно!", "ARK CFG EDITOR", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                CFogLIGHT = false;
            }
        }

        private void CBFoliage_Check(object sender, RoutedEventArgs e)
        {
            if (CBFoliage.IsChecked == true)
            {
                CFoliage = true;
                if (Warnings) 
                {
                    MessageBox.Show("Некоторые камни так-же как и кусты с некоторыми деревьями полностью пропадут!", "ARK CFG EDITOR", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                CFoliage = false;
            }
        }

        private void CBWaterSurface_Check(object sender, RoutedEventArgs e)
        {
            if (CBWaterSurface.IsChecked == true)
            {
                CWaterSurface = true;
                if (Warnings)
                {
                    MessageBox.Show("Отображение снарядов будет полностью отключено, вы не будете видеть лучи на месте своей смерти и след от дропов!", "ARK CFG EDITOR", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                CWaterSurface = false;
            }
        }

        private void CBOptimization_Check(object sender, RoutedEventArgs e)
        {
            if (CBOptimization.IsChecked == true)
            {
                COptimization = true;
            }
            else
            {
                COptimization = false;
            }
        }

        private void CBFPSCap_Check(object sender, RoutedEventArgs e)
        {
            if (CBFPSCap.IsChecked == true)
            {
                CNoCAP = true;
            }
            else
            {
                CNoCAP = false;
            }
        }

        private void CBLimitSettings_Check(object sender, RoutedEventArgs e)
        {
            if (CBLimitSettings.IsChecked == true)
            {
                CLimitSettings = true;
            }
            else
            {
                CLimitSettings = false;
            }
        }

        private void CBOFFWarnings_Check(object sender, RoutedEventArgs e)
        {
            if (CBOFFWarnings.IsChecked == true)
            {
                Warnings = false;
            }
            else
            {
                Warnings = true;
            }
        }
        #endregion
    }
}
