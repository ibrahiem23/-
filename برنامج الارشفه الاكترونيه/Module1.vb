Imports System.Data.OleDb
Module Module1
    Public CON As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DATA_COM.mdb")
    Public DT As New DataTable
    Public DA As New OleDb.OleDbDataAdapter

    Public Function CODE_GENE(T_NAME, ID_) As Integer
        CODE_GENE = 0
        Dim DT As New DataTable
        Dim DA As New OleDb.OleDbDataAdapter("SELECT * FROM " & T_NAME & " ORDER BY " & ID_ & "", CON)
        DA.Fill(DT)
        If DT.Rows.Count <> 0 Then
            Dim I = DT.Rows.Count - 1
            CODE_GENE = Val(DT.Rows(I).Item(ID_))

        End If
    End Function

    Public Sub FILL_DVG(DVG As DataGridView, OLEDB As String)
        DVG.DataSource = ""
        Dim DT As New DataTable
        Dim DA As New OleDbDataAdapter(OLEDB, CON)
        DA.Fill(DT)
        DVG.AutoGenerateColumns = False
        DVG.DataSource = DT.DefaultView

    End Sub




End Module
