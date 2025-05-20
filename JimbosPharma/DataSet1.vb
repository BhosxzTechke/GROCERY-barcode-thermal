Partial Class DataSet1
    Partial Class dtpaymentDataTable

        'Private Sub dtpaymentDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
        '    If (e.Column.ColumnName = Me.DataColumn1Column.ColumnName) Then
        '        vat() 'Add user code here
        '    End If

        'End Sub




        Private Sub dtpaymentDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.usersColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
