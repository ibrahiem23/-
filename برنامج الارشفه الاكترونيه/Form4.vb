Public Class Form4
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Select()
        FILL_DVG(DataGridView1, "SELECT * FROM EMPLOYEE")
        TextBox1.Text = CODE_GENE("EMPLOYEE", "رقم_الموظف") + 1
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
            Dim DA As New OleDb.OleDbDataAdapter("SELCET * FROM EMPLOYEE WHERE الأسم_كاملا ='" & TextBox2.Text & "'", CON)
            DA.Fill(DT)
            If DT.Rows.Count > 0 Then
            Else
                Dim DR = DT.NewRow
                DR!رقم_الموظف = TextBox1.Text
                DR!الأسم_كاملا = TextBox2.Text
                DR!الهويه_الوطنيه = TextBox3.Text
                DR!الاداره = TextBox4.Text
                DR!رقم_الجوال = TextBox5.Text
                DT.Rows.Add(DR)
                Dim SAVE As New OleDb.OleDbCommandBuilder(DA)
                SAVE.QuotePrefix = "["
                SAVE.QuoteSuffix = "]"
                DA.Update(DT)
                DT.AcceptChanges()
                Button1_Click(Nothing, Nothing)
                DT.Clear()
                FILL_DVG(DataGridView1, "SELECT * FROM EMPLOYEE")
                MessageBox.Show("تم حفظ البيانات بنجاح", "رساله تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information)


            End If






        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim DT As New DataTable
            Dim DA As New OleDb.OleDbDataAdapter("SELECT * FROM EMPLOYEE WHERE الأسم كاملا = '" & TextBox2.Text & "'", CON)
            DA.Fill(DT)
            If DT.Rows.Count = 0 Then
            Else
                Dim DR = DT.Rows(0)
                DR!رقم_الموظف = TextBox1.Text
                DR!الأسم_كاملا = TextBox2.Text
                DR!الهويه_الوطنيه = TextBox3.Text
                DR!الاداره = TextBox4.Text
                DR!رقم_الجوال = TextBox5.Text
                Dim SAVE As New OleDb.OleDbCommandBuilder(DA)
                SAVE.QuotePrefix = "["
                SAVE.QuoteSuffix = "]"
                DA.Update(DT)
                DT.AcceptChanges()
                Button1_Click(Nothing, Nothing)
                DT.Clear()
                FILL_DVG(DataGridView1, "SELECT * FROM EMPLOYEE")
                MessageBox.Show("تم تعديل البيانات بنجاح", "رساله تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            If MessageBox.Show("هل انت متأكد من حذف البيانات الحاليه؟", "رساله تنبيه", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub
            Dim DT As New DataTable
            Dim DA As New OleDb.OleDbDataAdapter("SELECT * FROM EMPLOYEE WHERE الأسم_كاملا ='" & TextBox2.Text & "'", CON)
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
                FILL_DVG(DataGridView1, "SELECT * FROM EMPLOYEE")
                MessageBox.Show("تم حذف البيانات بنجاح", "رساله تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If



        Catch ex As Exception

        End Try
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

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        If RadioButton1.Checked = True Then
            Dim DT As New DataTable
            Dim DA As New OleDb.OleDbDataAdapter("SELECT * FROM EMPLOYEE WHERE الأسم_كاملا LIKE '%" & TextBox6.Text & "%'", CON)
            DA.Fill(DT)
            DataGridView1.DataSource = DT.DefaultView
        End If
        If RadioButton2.Checked = True Then
            Dim DT As New DataTable
            Dim DA As New OleDb.OleDbDataAdapter("SELECT * FROM EMPLOYEE WHERE الهويه_الوطنيه LIKE '%" & TextBox6.Text & "%'", CON)
            DA.Fill(DT)
            DataGridView1.DataSource = DT.DefaultView
        End If

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class