﻿// Copyright (c) 2010-2013 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using SharpDX;

namespace Project
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public LabGame game;
        public MainMenu mainMenu;
        public InGameUI inGameUI;
        public MainPage()
        {
            InitializeComponent();
            game = new LabGame(this);
            game.Run(this);
            mainMenu = new MainMenu(this);
            //inGameUI = new InGameUI(this,game);
            this.Children.Add(mainMenu);
        }

        // TASK 1: Update the game's score
        public void UpdateScore(int score)
        {
            txtScore.Visibility = Visibility.Collapsed;
            debuggingBlock.Visibility = Visibility.Collapsed;
            txtScore.Text = "Score: " + score.ToString();
            debuggingBlock.Text = "Sphere pos: " + game.sphere.pos.ToString() + "\n" + "Camera pos: " + game.camera.pos.ToString() + "\n" + "Accelerometer X: " + game.accelerometerReading.AccelerationX.ToString() + "\n" + "Accelerometer Y: " + game.accelerometerReading.AccelerationY.ToString();
            debuggingBlock.Text += "\nxSpeed: " + game.sphere.xSpeed.ToString() + "\nzSpeed: " + game.sphere.zSpeed.ToString() + "\nxAngle: " + game.sphere.xAngle.ToString() + "\nzAngle: " + game.sphere.zAngle.ToString() + "\nSphere radius: " + game.sphere.radius.ToString();
            debuggingBlock.Text += "\n seed " + game.mazeSeed;
            debuggingBlock.Text += "\n a Vertex: " + game.mazeLandscape.normalMaze[0].ToString() + "\n next pos type: " + game.sphere.nextPosType.ToString();
            //debuggingBlock.Text += "\nNumber of meshes in sphere model: " + game.sphere.model.Meshes.Count.ToString() + "\nSphere center: " + game.sphere.model.Meshes[0].BoundingSphere.Center.ToString() + "\nSphere radius: " + game.sphere.model.Meshes[0].BoundingSphere.Radius.ToString();
            debuggingBlock.Text += "\n dimension : " + game.mazeDimension;
            debuggingBlock.Text += "\n left corner : " + game.sphere.leftUpCorner2.X;
            debuggingBlock.Text += "\n left corner : " + game.sphere.leftUpCorner2.Y;
            debuggingBlock.Text += "\n In left Up : " + game.sphere.inLeftUp;
        }

        // TASK 2: Starts the game.  Not that it seems easier to simply move the game.Run(this) command to this function,
        // however this seems to result in a reduction in texture quality on some machines.  Not sure why this is the case
        // but this is an easy workaround.  Not we are also making the command button invisible after it is clicked
        public void StartGame()
        {
            game.reCreate();
            this.Children.Remove(mainMenu);
            game.started = true;
            game.resumed = true;
        }

        async void OnSuspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            var Deferral = e.SuspendingOperation.GetDeferral();

            using (var Device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Hardware, SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport, new[] { SharpDX.Direct3D.FeatureLevel.Level_11_1, SharpDX.Direct3D.FeatureLevel.Level_11_0 }))
            using (var Direct3DDevice = Device.QueryInterface<SharpDX.Direct3D11.Device1>())
            using (var DxgiDevice3 = Direct3DDevice.QueryInterface<SharpDX.DXGI.Device3>())
                DxgiDevice3.Trim();

            Deferral.Complete();
        }
    }
}
