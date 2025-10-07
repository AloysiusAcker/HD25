Option Strict Off
Option Explicit On
Namespace WebGestor
    Public Module Module_Constantes
        Public BDEmpresa As String = "BDGEmpresa3TG"
        Public BDGrupoEmpresa As String = "BDGrupoEmpresas"
        Public BDSeguridadEmpresa As String = "BDSeguridadGrupoEmps"
        Public NomEmpresa As String = "Tecnologías"
        Public NomServer As String = "HAC-DATA01\TECNOLOGIAS" 'SHC2D\BANCO" ' "" 'TEC07SRV81A\TECNOLOGIAS  SERTRANPE47\TECNOLOGIAS
        Public Ruta_GrEmp As String = "workstation id=;packet size=4096;user id=sa;data source=" & NomServer & ";persist security info=False; POOLING=FALSE;initial catalog=BDGrupoEmpresas"
        Public Ruta_Ng As String = "workstation id=;packet size=4096;user id=sa;data source=" & NomServer & ";persist security info=False;POOLING=FALSE;initial catalog=BDSeguridadGrupoEmps"
        Public strConexion As String = "workstation id=;packet size=4096;user id=sa;data source=" & NomServer & ";persist security info=False;POOLING=FALSE;initial catalog=BDGEmpresa3TC"
        Public strConexionStarbucks As String = "workstation id=;packet size=4096;XpoProvider=MSSqlServer; data source=hac-data01\starbucks;user id=sa;password=; initial catalog=BDGEmpresa1TE;Persist Security Info=true"
        'Public Ruta_Ng As String = "workstation id=;packet size=4096;user id=sa;data source=" & NomServer & ";persist security info=False;initial catalog=BDg"
        'Public Ruta_Emp As String
        'Public CmdGlobal As New SqlClient.SqlCommand
        'Public CmdGlobal_GpEmp As New SqlClient.SqlCommand
    End Module
End Namespace

