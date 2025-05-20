Imports System.Windows.Forms

' Custom column for handling DateTimePicker in DataGridView
Public Class DataGridViewCalendarColumn
    Inherits DataGridViewColumn

    ' Constructor for the custom calendar column
    Public Sub New()
        MyBase.New(New DataGridViewCalendarCell())
    End Sub

    Public Overrides Property CellTemplate As DataGridViewCell
        Get
            Return MyBase.CellTemplate
        End Get
        Set(value As DataGridViewCell)
            ' Ensure that the cell used for the template is a DataGridViewCalendarCell
            If Not (TypeOf value Is DataGridViewCalendarCell) Then
                Throw New InvalidCastException("Must be a DataGridViewCalendarCell")
            End If
            MyBase.CellTemplate = value
        End Set
    End Property
End Class

' Custom cell to handle DateTimePicker control in a cell
Public Class DataGridViewCalendarCell
    Inherits DataGridViewTextBoxCell

    ' Use the short date format
    Public Sub New()
        Me.Style.Format = "d"
    End Sub

    Public Overrides Sub InitializeEditingControl(rowIndex As Integer, _
                                                  initialFormattedValue As Object, _
                                                  dataGridViewCellStyle As DataGridViewCellStyle)
        ' Set up the editing control to be a DateTimePicker
        MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle)
        Dim control As DataGridViewCalendarEditingControl = CType(DataGridView.EditingControl, DataGridViewCalendarEditingControl)
        If Me.Value Is Nothing OrElse IsDBNull(Me.Value) Then
            control.Value = DateTime.Now
        Else
            control.Value = CType(Me.Value, DateTime)
        End If
    End Sub

    Public Overrides ReadOnly Property EditType As Type
        Get
            ' Return the type of the editing control
            Return GetType(DataGridViewCalendarEditingControl)
        End Get
    End Property

    Public Overrides ReadOnly Property ValueType As Type
        Get
            ' Return the type of the value
            Return GetType(DateTime)
        End Get
    End Property

    Public Overrides ReadOnly Property DefaultNewRowValue As Object
        Get
            ' Default value for a new row
            Return DateTime.Now
        End Get
    End Property
End Class
