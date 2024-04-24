Imports System.Data.OleDb


Public Class Form2
    Public CON As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DATA_COM.mdb")
    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Select()
        FILL_DVG(DataGridView1, "SELECT * FROM DATA_INFO")
        TextBox1.Text = CODE_GENE("DATA_INFO", "الرقم") + 1


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim DT As New DataTable
            Dim query As String = "SELECT * FROM DATA_INFO "
            Dim DA As New OleDbDataAdapter(query, CON)
            DA.SelectCommand.Parameters.Add("@Number", OleDbType.VarChar, 255).Value = TextBox1.Text
            DA.SelectCommand.Parameters.Add("@Name", OleDbType.VarChar, 255).Value = TextBox2.Text
            DA.SelectCommand.Parameters.Add("@PhoneNumber", OleDbType.VarChar, 255).Value = TextBox3.Text
            DA.SelectCommand.Parameters.Add("@Address", OleDbType.VarChar, 255).Value = TextBox4.Text
            DA.Fill(DT)

            If DT.Rows.Count = 0 Then
                ' Dim newRow = DT.NewRow()
                ' newRow("الرقم") = TextBox1.Text
                'newRow("الأسم") = TextBox2.Text
                'newRow("رقم _الهاتف") = TextBox3.Text
                ' newRow("العنوان _الرئيسي") = TextBox4.Text
                ' DT.Rows.Add(newRow)
                Dim cmdInsert As New OleDb.OleDbCommand()
                cmdInsert.Connection = CON
                cmdInsert.CommandText = $"INSERT INTO DATA_INFO  VALUES ({TextBox1.Text}, {TextBox2.Text}, {TextBox3.Text},{TextBox4.Text})"


                cmdInsert.Parameters.Add("@Number", OleDbType.VarChar, 255).Value = TextBox1.Text
                cmdInsert.Parameters.Add("@Name", OleDbType.VarChar, 255).Value = TextBox2.Text
                cmdInsert.Parameters.Add("@PhoneNumber", OleDbType.VarChar, 255).Value = TextBox3.Text
                cmdInsert.Parameters.Add("@Address", OleDbType.VarChar, 255).Value = TextBox4.Text

                CON.Open()
                cmdInsert.ExecuteNonQuery()
                CON.Close()



                ' Assuming Button1_Click does some sort of refresh or navigation action
                Button1_Click(Nothing, Nothing)

                ' Assuming FILL_DVG updates the DataGridView with the new data
                FILL_DVG(DataGridView1, "SELECT * FROM DATA_INFO")

                MessageBox.Show("تم حفظ البيانات بنجاح", "رسالة تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("البيانات بالفعل موجودة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء حفظ البيانات: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim DT As New DataTable
            Dim DA As New OleDb.OleDbDataAdapter("SELECT * FROM DATA_INFO WHERE الأسم= '" & TextBox2.Text & "'", CON)
            DA.Fill(DT)
            If DT.Rows.Count = 0 Then
            Else
                Dim DR = DT.Rows(0)
                DR!الرقم = TextBox1.Text
                DR!الأسم = TextBox2.Text
                DR!رقم_الهاتف = TextBox3.Text
                DR!العنوان_الرئيسي = TextBox4.Text
                Dim SAVE As New OleDb.OleDbCommandBuilder(DA)
                DA.Update(DT)
                DT.AcceptChanges()
                Button1_Click(Nothing, Nothing)
                DT.Clear()
                FILL_DVG(DataGridView1, "SELECT * FROM DATA_INFO")
                MessageBox.Show("رساله تأكيد", "تم تعديل البيانات بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            If MessageBox.Show("رساله تنبيه", " هل تريد حذف السجل الحالي", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub
            Dim DT As New DataTable
            Dim DA As New OleDb.OleDbDataAdapter("SELECT * FROM DATA_INFO WHERE الأسم= '" & TextBox2.Text & "'", CON)
            DA.Fill(DT)
            If DT.Rows.Count = 0 Then
            Else
                Dim DR = DT.Rows(0)
                DR.Delete()
                Dim SAVE As New OleDb.OleDbCommandBuilder(DA)
                DA.Update(DT)
                DT.AcceptChanges()
                DT.Clear()
                FILL_DVG(DataGridView1, "SELECT * FROM DATA_INFO")
                Button1_Click(Nothing, Nothing)
                MessageBox.Show("رساله تنبيه", "تم حذف البينانات بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)


            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If RadioButton1.Checked = True Then
            Dim DT As New DataTable
            Dim DA As New OleDb.OleDbDataAdapter("SELECT * FROM DATA_INFO WHRE الأسم LIKE '%" & TextBox5.Text & "%'", CON)
            DA.Fill(DT)
            DataGridView1.DataSource = DT.DefaultView
        End If
        If RadioButton2.Checked = True Then
            Dim DT As New DataTable
            Dim DA As New OleDb.OleDbDataAdapter("SELECT * FROM DATA_INFO WHERE رقم_الهاتف LIKE '%" & TextBox5.Text & "%'", CON)
            DA.Fill(DT)
            DataGridView1.DataSource = DT.DefaultView



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

            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Dispose()
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class
