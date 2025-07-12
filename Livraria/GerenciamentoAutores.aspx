<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GerenciamentoAutores.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoAutores" %>

<dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="50%" Theme="Office365">
    <Items>
        <dx:LayoutGroup Caption="" ColCount="2" SettingsItemCaptions-Location="Top">
            <Items>
                <dx:LayoutItem Caption="Nome">
                    <ParentContainerStyle Paddings-PaddingRight="12"></ParentContainerStyle>
                    <LayoutItemNestedControlContainer>
                        <dx:ASPxTextBox ID="txbCadastroNomeAutor" runat="server" Width="100%">
                            <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                <RequiredField IsRequired="True" ErrorText="Digite o nome do Autor!" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </LayoutItemNestedControlContainer>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Sobrenome">
                    <ParentContainerStyle Paddings-PaddingLeft="0" Paddings-PaddingRight="12"></ParentContainerStyle>
                    <LayoutItemNestedControlContainer>
                        <dx:ASPxTextBox ID="txbCadastroSobrenomeAutor" runat="server" Width="100%">
                            <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                <RequiredField IsRequired="True" ErrorText="Digite o sobrenome do Autor!" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </LayoutItemNestedControlContainer>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="E-Mail" ColSpan="2">
                    <LayoutItemNestedControlContainer>
                        <dx:ASPxTextBox ID="txbCadastroEmailAutor" runat="server" Width="100%">
                            <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                <RequiredField IsRequired="True" ErrorText="Digite o email do Autor!" />
                                <RegularExpression ErrorText="Email inválido" ValidationExpression="[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </LayoutItemNestedControlContainer>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="" ColSpan="2">
                    <LayoutItemNestedControlContainer>
                        <dx:ASPxButton ID="btnSalvar" runat="server" Text="Salvar" AutoPostBack="false" Width="100%" OnClick="BtnNovoAutor_click" />
                    </LayoutItemNestedControlContainer>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
    </Items>
</dx:ASPxFormLayout>



</asp:Content>

