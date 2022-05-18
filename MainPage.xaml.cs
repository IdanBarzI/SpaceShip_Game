using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GameNew
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private GameLogic logic;
        private Board board;
        private Player player;
        private List<GameElem> shots;
        private List<GameElem> astroids;
        private List<GameElem> meteors;

        private DispatcherTimer mainTimer = new DispatcherTimer();
        private DispatcherTimer astroTimer = new DispatcherTimer();
        private DispatcherTimer shootTimer;


        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;

            player = new Player();
            astroids = new List<GameElem>();
            shots = new List<GameElem>();
            meteors = new List<GameElem>();
            board = new Board(MyCanvas, damage, score);
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            logic = new GameLogic();
            player.PlayerLocationSpawn(MyCanvas);
            board.AddElement(player);
            mainTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            mainTimer.Tick += Enemy_Player_Collision_Timer_Tick;

            shootTimer = new DispatcherTimer();
            shootTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            shootTimer.Tick += Shoot_Collision_Timer_Tick;

            Random rand = new Random();
            int astroSpawn = rand.Next(1, 3);

            astroTimer.Interval = new TimeSpan(0, 0, 0, astroSpawn);
            astroTimer.Tick += Enemy_Spawn_Timer_Tick;

            StartTheGame();
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            var key = args.VirtualKey;
            if (key == VirtualKey.Left || key == VirtualKey.Right)
            {
                player.Move(key, MyCanvas.ActualWidth);
            }
            else if (key == VirtualKey.Space)
            {
                Shot shot = new Shot(player.X, player.Y);
                shots.Add(shot);
                board.AddElement(shot);
            }
            else if (key == VirtualKey.Escape)
            {
                ExitGame();
            }
        }

        public void StartTheGame()
        {
            mainTimer.Start();
            astroTimer.Start();
            shootTimer.Start();

        }

        public void StopTheGame()
        {
            mainTimer.Stop();
            astroTimer.Stop();
            shootTimer.Stop();
        }

        public void ExitGame()
        {
            Application.Current.Exit();
        }

        public void AddEnemy(Enemy enemie, List<GameElem> enemies) //למה הפעולה לא עובדת אלא אם אני שם את המחלקה GAMEELEM בחחימה public
        {
            enemies.Add(enemie);
            board.AddElement(enemie);
        }

        public void RemoveGameElements(List<GameElem> enemies, List<GameElem> tmpEnemies) //למה הפעולה לא עובדת אלא אם אני שם את המחלקה GAMEELEM בחחימה public
        {
            foreach (var tmpEnemy in tmpEnemies)
            {
                enemies.Remove(tmpEnemy);
                board.RemoveElement(tmpEnemy);
            }
        }

        public List<GameElem> GetGameElementToRemove(List<GameElem> gameElems,GameElem shot = null)
        {
            List<GameElem> tmpGameElements = new List<GameElem>();
            List<GameElem> tmpShots = new List<GameElem>();
            foreach (var gameElement in gameElems)
            {

                if (board.IsOutOfScreen(gameElement))
                {
                    tmpGameElements.Add(gameElement); 
                }
                if (shot != null && logic.IsCollition(shot, gameElement))
                {
                    tmpGameElements.Add(gameElement);

                }
            }
            return tmpGameElements;

        }

        private void Enemy_Spawn_Timer_Tick(object sender, object e)
        {
            Random rnd = new Random();
            for (int i = 0; i < rnd.Next(10, 15); i++)
            {
                Astroid astroid = new Astroid(MyCanvas.ActualWidth);
                AddEnemy(astroid, astroids);
            }
            for (int i = 0; i < rnd.Next(3, 8) && meteors.Count < 8; i++)
            {
                Meterorit meterorit = new Meterorit((int)MyCanvas.ActualHeight, (int)MyCanvas.ActualWidth);
                AddEnemy(meterorit, meteors);
            }

            var tmpAstroids = GetGameElementToRemove(astroids);
            var tmpMeterorits = GetGameElementToRemove(meteors);
            RemoveGameElements(astroids, tmpAstroids);
            RemoveGameElements(meteors, tmpMeterorits);

        }

        private void Shoot_Collision_Timer_Tick(object sender, object e)
        {
            List<GameElem> shootsTmp = new List<GameElem>();
            List<GameElem> AstroidsTmp = new List<GameElem>();
            List<GameElem> meteoritsTmp = new List<GameElem>();
            foreach (var shot in shots)
            {
                shot.MoveElem();
                List<GameElem> tmpAstroids = GetGameElementToRemove(astroids, shot);
                AstroidsTmp.AddRange(tmpAstroids);
                foreach (var astro in astroids)
                {
                    if (logic.IsCollition(shot, astro))
                    {
                        board.RemoveElement(astro);
                        board.RemoveElement(shot);
                        meteoritsTmp.Add(astro);
                        shootsTmp.Add(shot);
                        UpdateScore((Enemy)astro);
                    }
                }

                foreach (var mete in meteors)
                {
                    if (logic.IsCollition(shot, mete))
                    {
                        board.RemoveElement(mete);
                        board.RemoveElement(shot);
                        meteoritsTmp.Add(mete);
                        shootsTmp.Add(shot);
                        UpdateScore((Enemy)mete);
                    }
                }
            }
            foreach (var tempMeteorit in meteoritsTmp)
            {
                meteors.Remove(tempMeteorit);
            }

            foreach (var tempAstroid in AstroidsTmp)
            {
                astroids.Remove(tempAstroid);
            }

            RemoveGameElements(shots, GetGameElementToRemove(shots));

            foreach (var tempShot in shootsTmp)
            {
                shots.Remove(tempShot);
            }
            if (logic.IsWin())
            {
                Win();
            }
        }

        private void Enemy_Player_Collision_Timer_Tick(object sender, object e)
        {
            List<GameElem> tmpAstroids = new List<GameElem>();
            List<GameElem> tmpMeterorits = new List<GameElem>();
            foreach (var astroid in astroids)
            {
                astroid.MoveElem();
                if (logic.IsCollition(astroid, player))
                {
                    UpdateDamage((Enemy)astroid);
                    tmpAstroids.Add(astroid);
                    if (logic.IsLose())
                    {
                        Lose();
                        break;
                    }

                }
                else if (board.IsOutOfScreen(astroid))
                {
                    tmpAstroids.Add(astroid);
                }
            }
            foreach (Meterorit meterorit in meteors)
            {
                meterorit.MoveElem();
                if (logic.IsCollition(meterorit, player))
                {
                    UpdateDamage(meterorit);
                    tmpMeterorits.Add(meterorit);
                    if (logic.IsLose())
                    {
                        Lose();
                        break;
                    }

                }
            }

            RemoveGameElements(meteors, tmpMeterorits);
            RemoveGameElements(astroids, tmpAstroids);
        }

        private void UpdateScore(Enemy enemy)
        {
            logic.AddScoreUntilWin(enemy.GetScore());
            board.UpdateScoreText(logic.Score);

        }

        private void UpdateDamage(Enemy enemy)
        {
            logic.AddDamageUntilLose(enemy.GetDamage());
            board.UpdateDamageText(logic.Damage);
        }

        private void ClearElements() // Clear all the elements exept player
        {
            board.RemoveAll(astroids);
            board.RemoveAll(meteors);
            board.RemoveAll(shots);
            astroids.Clear();
            meteors.Clear();
            shots.Clear();
        }

        public async void Lose()
        {
            StopTheGame();
            ClearElements();
            board.UpdatetBackGround(@"ms-appx:///Assets/LoseStar.gif");
            player.UpdatetImage(@"ms-appx:///Assets/Blast.gif");

            MessageDialog showDialog = new MessageDialog($"You Lost ,Do You want to play again?");
            showDialog.Commands.Add(new UICommand("Yes")
            {
                Id = 0
            });
            showDialog.Commands.Add(new UICommand("No")
            {
                Id = 1
            });
            showDialog.DefaultCommandIndex = 0;
            showDialog.CancelCommandIndex = 1;

            var result = await showDialog.ShowAsync();
            if ((int)result.Id == 0)
            {
                NewGame();
            }
            else
            {
                ExitGame();
            }
        }

        public async void Win()
        {
            StopTheGame();
            ClearElements();
            board.UpdatetBackGround(@"ms-appx:///Assets/WinStar.gif");
            player.UpdatetImage(@"ms-appx:///Assets/Player1.gif");

            MessageDialog showDialog = new MessageDialog("You win ,Do You want to play again?");
            showDialog.Commands.Add(new UICommand("Yes")
            {
                Id = 0
            });
            showDialog.Commands.Add(new UICommand("No")
            {
                Id = 1
            });
            showDialog.DefaultCommandIndex = 0;
            showDialog.CancelCommandIndex = 1;
            var result = await showDialog.ShowAsync();
            if ((int)result.Id == 0)
            {
                NewGame();
            }
            else
            {
                Application.Current.Exit();
            }
        }

        public void NewGame()
        {
            board.RemoveElement(player);
            player = new Player();
            board.AddElement(player);
            player.PlayerLocationSpawn(MyCanvas);
            logic.Score = 0;
            score.Text = logic.Score.ToString();
            logic.Damage = 0;
            damage.Text = logic.Damage.ToString();

            board.UpdatetBackGround(@"ms-appx:///Assets/SpaceBackGround.gif");

            player.UpdatetImage(@"ms-appx:///Assets/Player1.png");

            StartTheGame();
        }

    }


}

