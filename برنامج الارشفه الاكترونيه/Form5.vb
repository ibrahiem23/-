Imports System.Data.OleDb
Public Class Form5
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click, Label5.Click, Label4.Click, Label3.Click, Label2.Click, Label6.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Shell("wiaacmgr.exe", AppWinStyle.NormalFocus)
    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Select()
        FILL_DVG(DataGridView1, "SELECT * FROM ADD_DECM")
        TextBox1.Text = CODE_GENE("ADD_DECM", "الرقم_التسلسلي") + 1
    End Sub

    Private Sub AxAcroPDF1_Enter(sender As Object, e As EventArgs) Handles AxAcroPDF1.Enter

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        AxAcroPDF1.src = ""
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Dim DT As New DataTable
            Dim DA As New OleDb.OleDbDataAdapter("SELECT * FROM ADD_DEM WHERE صاحب_الملف = '" & TextBox3.Text & "'", CON)
            DA.Fill(DT)
            If DT.Rows.Count > 0 Then
            Else
                Dim DR = DT.NewRow
                DR!الرقم_التسلسلي = TextBox1.Text
                DR!رقم_الملف = TextBox2.Text
                DR!صاحب_الملف = TextBox3.Text
                DR!الصف = TextBox4.Text
                DR!الرف = TextBox5.Text
                DR!الاداره = TextBox6.Text
                DR!PDF = AxAcroPDF1.src
                DT.Rows.Add(DR)
                Dim SAVE As New OleDb.OleDbCommandBuilder(DA)
                SAVE.QuotePrefix = "["
                SAVE.QuoteSuffix = "]"
                DA.Update(DT)
                DT.AcceptChanges()
                Button6_Click(Nothing, Nothing)
                DT.Clear()
                FILL_DVG(DataGridView1, "SELCET * FROM ADD_DECM")
                MessageBox.Show("تم حفظ بيانات المستند بنجاح", "رساله تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "فشل في عمليه الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Error)


        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim DT As New DataTable
            Dim DA As New OleDb.OleDbDataAdapter("SELECT * FROM ADD_DEM WHERE صاحب_الملف = '" & TextBox3.Text & "'", CON)
            DA.Fill(DT)
            If DT.Rows.Count = 0 Then
            Else

                Dim DR = DT.Rows(0)
                DR!الرقم_التسلسلي = TextBox1.Text
                DR!رقم_الملف = TextBox2.Text
                DR!صاحب_الملف = TextBox3.Text
                DR!الصف = TextBox4.Text
                DR!الرف = TextBox5.Text
                DR!الاداره = TextBox6.Text
                DR!PDF = AxAcroPDF1.src
                DT.Rows.Add(DR)
                Dim SAVE As New OleDb.OleDbCommandBuilder(DA)
                SAVE.QuotePrefix = "["
                SAVE.QuoteSuffix = "]"
                DA.Update(DT)
                DT.AcceptChanges()
                Button6_Click(Nothing, Nothing)
                DT.Clear()
                FILL_DVG(DataGridView1, "SELCET * FROM ADD_DECM")
                MessageBox.Show("تم تعديل بيانات المستند بنجاح", "رساله تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "فشل في عمليه التعديل", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            If MessageBox.Show("هل انت متأكد من حذف المستند الحالي؟", "رساله تنبيه", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub
            Dim DT As New DataTable
            Dim DA As New OleDb.OleDbDataAdapter("SELECT * FROM ADD_DECM WHERE صاحب_الملف ='" & TextBox3.Text & "'", CON)
            DA.Fill(DT)
            If DT.Rows.Count = 0 Then
            Else
                Dim DR = DT.Rows(0)
                DR.Delete()
                Dim SAVE As New OleDb.OleDbCommandBuilder(DA)
                SAVE.QuotePrefix = "["
                SAVE.QuoteSuffix = "]"
                DA.Update(DT)
                DT.AcceptChanges()
                Button1_Click(Nothing, Nothing)
                DT.Clear()
                FILL_DVG(DataGridView1, "SELECT * FROM ADD_DECM")
                MessageBox.Show("تم حذف المستند بنجاح", "رساله تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "فشل في عمليه الحذف", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OpenFileDialog1.Filter = "PDF FILES|*.PDF"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            AxAcroPDF1.src = OpenFileDialog1.FileName
            AxAcroPDF1.src = AxAcroPDF1.src
        End If
    End Sub


    Private Sub DataGridView1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded
        For I As Integer = 0 To DataGridView1.Rows.Count - 1
            DataGridView1.Rows(I).Cells(0).Value = "عرض البيانات"
            Dim ROW As DataGridViewRow = DataGridView1.Rows(I)

        Next
    End Sub



    Private Sub DataGridView1_Click(sender As Object, e As EventArgs) Handles DataGridView1.Click
        Try
            If DataGridView1(0, DataGridView1.CurrentRow.Index).Selected = True Then
                TextBox1.Text = (DataGridView1.CurrentRow.Cells(1).Value)
                TextBox2.Text = (DataGridView1.CurrentRow.Cells(2).Value)
                TextBox3.Text = (DataGridView1.CurrentRow.Cells(3).Value)
                TextBox4.Text = (DataGridView1.CurrentRow.Cells(4).Value)
                TextBox5.Text = (DataGridView1.CurrentRow.Cells(5).Value)
                TextBox6.Text = (DataGridView1.CurrentRow.Cells(6).Value)
                AxAcroPDF1.src = (DataGridView1.CurrentRow.Cells(7).Value)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        If Trim(TextBox6.Text) <> "" Then
            Dim DT As New DataTable
            DT.Clear()
            Dim DA As New OleDb.OleDbDataAdapter
            DA = New OleDbDataAdapter("SELECT * FROM ADD_DECM WHERE صاحب_الملف LIKE '%" & Trim$(TextBox7.Text) & "%'", CON)
            DA.Fill(DT)
            DataGridView1.DataSource = DT.DefaultView

        End If
    End Sub
End Class

