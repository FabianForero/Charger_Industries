<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Charger.aspx.cs" Inherits="ChargerIndustries.Charger" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
            <br />
            <asp:Button  runat="server" ID="btnUpload" Text="Upload" 
                onclick="btnUpload_Click" />
                <asp:Label runat="server" ID="lblMessage" Text=""></asp:Label>
            <br />
            <br />
            <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="table_selected"
                autopostback="true">
                <asp:ListItem Text="Select table to view..." Value="0"></asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>
    </form>
</body>
</html>
