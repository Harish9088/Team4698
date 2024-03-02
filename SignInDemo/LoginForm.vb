Public Class LoginForm
    Private DB As New DBAccess

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click

        If String.IsNullOrWhiteSpace(UsernameTextBox.Text) Then
            MessageBox.Show("Username can not be empty.")
            UsernameTextBox.SelectAll()
            UsernameTextBox.Focus()
            Exit Sub
        End If
        If String.IsNullOrWhiteSpace(PasswordTextBox.Text) Then
            MessageBox.Show("Password can not be empty.")
            UsernameTextBox.SelectAll()
            UsernameTextBox.Focus()
            Exit Sub
        End If

        DB.AddParam("@username", UsernameTextBox.Text)
        DB.AddParam("@password", PasswordTextBox.Text)
        DB.ExecuteQuery("SELECT * FROM staff WHERE username = ? AND password = ? ")

        If DB.Exception <> String.Empty Then
            MessageBox.Show("DB Error: " & DB.Exception)
            Exit Sub
        End If

        If DB.RecordCount > 0 Then
            'user has a match, can be logged in
            'clear username and password entered to secure the system.
            UsernameTextBox.Clear()
            PasswordTextBox.Clear()

            'hide the login form from user afer login. 
            'in the next form load event, it will close the login form completely. 
            Me.Hide()

            'store_id is just a representative field for user type or role. we use store_id here to process staff who belongs to different store. 
            If DB.DBDataTable(0)!store_id = 1 Then
                Store1StaffForm.ShowDialog()

            ElseIf DB.DBDataTable(0)!store_id = 2 Then
                Store2StaffForm.ShowDialog()
            End If


        Else
            'DB.RecordCount = 0, no username and password match with the login credentials
            MessageBox.Show("Username and password mismatch.")
            UsernameTextBox.SelectAll()
            UsernameTextBox.Focus()
            Exit Sub
        End If

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub UsernameTextBox_TextChanged(sender As Object, e As EventArgs) Handles UsernameTextBox.TextChanged

    End Sub

    Private Sub PasswordTextBox_TextChanged(sender As Object, e As EventArgs) Handles PasswordTextBox.TextChanged

    End Sub

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
