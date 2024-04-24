Imports System.Data.OleDb
Imports System.Runtime.Remoting.Messaging
Public Class Form3
    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Select()
        FILL_DVG(DataGridView1, "SELECT * FROM MANGMENT")
        TextBox1.Text = CODE_GENE("MANGMENT", "رقم_الأداره_القسم") + 1


    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click, Button7.Click
        DT.Clear()
        FILL_DVG(DataGridView1, "SELECT * FROM MANGMENT")
        MsgBox("تم تحديث البيانات بنجاح")
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        If Trim(TextBox6.Text) <> "" Then
            Dim DT As New DataTable
            DT.Clear()
            Dim DA As New OleDb.OleDbDataAdapter
            DA = New OleDbDataAdapter("SELECT * FROM MANGMENT WHERE المدير_الرئيس LIKE '%" & Trim$(TextBox6.Text) & "%'", CON)
            DA.Fill(DT)
            DataGridView1.DataSource = DT.DefaultView

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim DT As New DataTable
            Dim DA As New OleDb.OleDbDataAdapter("SELECT * FROM MANGMENT WHRER اسم_الاداره_القسم='" & TextBox2.Text & "'", CON)
            DA.Fill(DT)
            If DT.Rows.Count > 0 Then
            Else
                Dim DR = DT.NewRow
                DR!رقم_الأداره_القسم = TextBox1.Text
                DR!اسم_الاداره_القسم = TextBox2.Text
                DR!المدير_الرئيس = TextBox3.Text
                DR!رقم_الهاتف = TextBox4.Text
                DR!رقم_جوال_الرئيس = TextBox5.Text
                DT.Rows.Add(DR)
                Dim SAVE As New OleDb.OleDbCommandBuilder(DA)
                SAVE.QuotePrefix = "["
                SAVE.QuoteSuffix = "]"
                DA.Update(DT)
                DT.AcceptChanges()
                Button1_Click(Nothing, Nothing)
                DT.Clear()
                FILL_DVG(DataGridView1, "SELECT * FROM MANGMENT")
                MessageBox.Show("تم حفظ البينات بنجاح بلقاعده", "رساله تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information)



            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim DT As New DataTable
            Dim DA As New OleDb.OleDbDataAdapter("SELCET * FROM MANGMENT WHERE اسم_الاداره_القسم ='" & TextBox2.Text & "'", CON)
            DA.Fill(DT)
            If DT.Rows.Count = 0 Then
            Else


                Dim DR = DT.Rows(0)
                DR!رقم_الأداره_القسم = TextBox1.Text
                DR!اسم_الاداره_القسم = TextBox2.Text
                DR!المدير_الرئيس = TextBox3.Text
                DR!رقم_الهاتف = TextBox4.Text
                DR!رقم_جوال_الرئيس = TextBox5.Text
                Dim SAVE As New OleDb.OleDbCommandBuilder(DA)
                SAVE.QuotePrefix = "["
                SAVE.QuoteSuffix = "]"
                DA.Update(DT)
                DT.AcceptChanges()
                Button1_Click(Nothing, Nothing)
                DT.Clear()
                FILL_DVG(DataGridView1, "SELECT * FROM MANGMENT")
                MessageBox.Show("تم تعديل البيانات بنجاح في القاعده", "رساله تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information)


            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            If MessageBox.Show("هل انت متأكد من حذف البيانات الحاليه؟", "رساله تنبيه", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub
            Dim DT As New DataTable
            Dim DA As New OleDb.OleDbDataAdapter("SELECT * FROM MANGMENT WHERE اسم_الاداره_القسم= '" & TextBox2.Text & "'", CON)
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
                DT.Clear()
                FILL_DVG(DataGridView1, "SELECT * FROM MANGMENT")
                Button1_Click(Nothing, Nothing)
                MessageBox.Show("تم حذف البينات من القاعده بنجاح", "رساله تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
        Form1.Show()

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
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class