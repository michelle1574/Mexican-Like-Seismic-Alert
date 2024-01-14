
Imports System.Speech.Synthesis
Imports System.Media
Imports System.Collections.ObjectModel

Public Class Alert
    Dim CountdownSeconds As Integer
    Dim soundFilePath As String = "beep.wav"
    Dim soundPlayer As New SoundPlayer(soundFilePath)
    Dim synthesizer As New SpeechSynthesizer()
    Dim installedVoices As ReadOnlyCollection(Of InstalledVoice) = synthesizer.GetInstalledVoices()
    Dim selectedVoice As InstalledVoice = installedVoices.FirstOrDefault(
            Function(voice) voice.VoiceInfo.Gender = VoiceGender.Female AndAlso voice.VoiceInfo.Age = VoiceAge.Adult)
    Dim ObjectX As Integer
    Dim ObjectY As Integer
    Dim Xcalculate As Integer
    Dim Ycalculate As Integer


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        CountdownSeconds -= 1
        Seconds.Text = CountdownSeconds & "s"
        Xcalculate = Seconds.Size.Width / 2
        ObjectX = 960 - Xcalculate
        Ycalculate = Seconds.Size.Height / 2
        ObjectY = 540 - Ycalculate
        Seconds.Location = New Point(ObjectX, ObjectY)
        If selectedVoice IsNot Nothing Then
            synthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult)
            synthesizer.Speak(CountdownSeconds)
        End If
        ' soundPlayer.Play()

        If CountdownSeconds = 0 Then
            Timer1.Stop()
            ' soundPlayer.Play()
            Me.Close()
        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Timer1.Stop()
        Me.Close()
    End Sub

    Private Sub Alert_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If Integer.TryParse(Selection.EstimatedTimeBeforeSwaves.Text, CountdownSeconds) Then
            Label1.Text = Selection.ExpectedLevelOfShaking.Text
            Label3.Text = Selection.TextBox3.Text

            ' Start the Timer
            Timer1.Start()
        Else
            MessageBox.Show("Invalid value for EstimatedTimeBeforeSwaves.")
            Me.Close()
        End If
    End Sub
End Class
