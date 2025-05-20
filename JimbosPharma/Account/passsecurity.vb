Public Class passsecurity
    Private Sub passsecurity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbQuestion.Items.Add("What is your favorite sport?")
        cmbQuestion.Items.Add("What is your favorite color?")
        cmbQuestion.Items.Add("Where were you born?")
        cmbQuestion.Items.Add("What is your pet’s name?")
        cmbQuestion.Items.Add("What is your favorite food?")
        cmbQuestion.SelectedIndex = 0 ' Set default selection
    End Sub

    ' Hardcoded answers (admin-only)
    Private Function GetCorrectAnswer(question As String) As String
        Select Case question
            Case "What is your favorite sport?"
                Return "basketball"
            Case "What is your favorite color?"
                Return "blue"
            Case "What is your pet’s name?"
                Return "buddy"
            Case "What is your favorite food?"
                Return "pizza"
            Case Else
                Return ""
        End Select
    End Function

    ' Verify answer on button click
    Private Sub btnVerifys_Click(sender As Object, e As EventArgs) Handles btnVerifys.Click
        Dim selectedQuestion As String = cmbquestion.Text
        Dim userAnswer As String = txtanswer.Text.Trim().ToLower()
        Dim correctAnswer As String = GetCorrectAnswer(selectedQuestion).ToLower()

        If userAnswer = correctAnswer Then
            MessageBox.Show("Correct! You can now change the password.", "Verified", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            frmchangepass.ShowDialog()

        Else
            MessageBox.Show("Incorrect answer. Access Denied.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Me.Close()

    End Sub
End Class