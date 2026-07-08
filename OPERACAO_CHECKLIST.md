# Operacao do Servidor

## 1) Start padrao
1. Executar Server_Start.cmd
2. Aguardar inicializacao completa dos processos
3. Executar _ops_health_check.cmd

## 2) Validacao rapida apos start
1. Processos esperados: CustomCertificationServer, GlobalManager, MachineManager, DownloadServer, GatewayServer, FarmManager, AgentServer, SR_ShardManager, SR_GameServer
2. Portas esperadas: 32000, 15880, 15882, 15883, 8090
3. Verificar se nao ha novas linhas criticas em 2026-07-08_FatalLog.txt e ExceptionHandlerInfo.log

## 3) Rotina diaria
1. Executar _ops_rotate_logs.cmd
2. Validar se dumps antigos foram movidos para Log/Dump e Log/Archive
3. Conferir espaco em disco

## 4) Rollback rapido
1. Parar stack com Server_Stop.cmd
2. Reverter alteracoes recentes em server.cfg, Cert/ini e SMC/ServiceManager.cfg
3. Regerar packt.dat via Cert/Ini2Dat.bat quando houver mudanca em Cert/ini
4. Iniciar novamente com Server_Start.cmd
5. Rodar _ops_health_check.cmd

## 5) Checklist de incidente
1. Erro de Certification: validar IP de Certification em server.cfg e Cert/ini/srNodeType.ini
2. Erro no SMC (invalid user info): validar SMC/ServiceManager.cfg e credencial no TB_User
3. Queda de Shard/Game: validar billing mock (porta 8090) e mensagens em FatalLog
4. Limite por IP: validar MaxUserForNonePCBangIP em server.cfg

## 6) Comandos uteis
1. Health check: _ops_health_check.cmd
2. Rotacao de logs: _ops_rotate_logs.cmd
3. Start: Server_Start.cmd
4. Stop: Server_Stop.cmd
