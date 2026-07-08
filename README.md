# ServerFile - Guia de Operacao

Este repositorio contem um ambiente de servidor de jogo (stack legacy Windows) com multiplos servicos, painel SMC, modulo de certification e utilitarios de operacao.

## Visao geral

Principais executaveis:
- GlobalManager
- MachineManager
- DownloadServer
- GatewayServer
- FarmManager
- AgentServer
- SR_ShardManager
- SR_GameServer
- SMC
- CustomCertificationServer

Arquivos centrais:
- server.cfg (configuracao principal dos servicos)
- Server_Start.cmd (startup padrao)
- Server_Stop.cmd (shutdown padrao)
- Cert/packt.dat (pack de certification usado em runtime)
- SMC/ServiceManager.cfg (conexao do SMC com division manager)

## Requisitos do ambiente

- Windows com SQL Server ativo
- Bases presentes: SRO_ACCOUNT, SRO_SHARD, SRO_LOG
- Permissoes para executar .cmd e .ps1

## Start e stop

Start completo:
1. Executar Server_Start.cmd

Stop completo:
1. Executar Server_Stop.cmd

## Fluxo de alteracao no Certification

Quando editar arquivos em Cert/ini:
1. Alterar os .ini necessarios
2. Regerar o pack: Cert/Ini2Dat.bat
3. Reiniciar stack (Server_Stop.cmd e Server_Start.cmd)

## Scripts de operacao adicionados

Health check:
- _ops_health_check.cmd
- ops/health_check.ps1

Rotacao de logs/dumps:
- _ops_rotate_logs.cmd
- ops/rotate_logs.ps1

Runbook:
- OPERACAO_CHECKLIST.md

## Configuracoes importantes aplicadas

- Certification e servicos internos alinhados para IP LAN: 192.168.1.101
- Billing fallback local em: http://127.0.0.1:8090/billing/
- Limite por IP configurado em GlobalManager:
  - MaxUserForNonePCBangIP = 1

## Logs e diagnostico

Arquivos relevantes:
- 2026-07-08_FatalLog.txt
- ExceptionHandlerInfo.log
- Log/FatalLog/
- Log/Dump/

Dicas:
1. Se houver queda de Shard/Game, verifique FatalLog e ExceptionHandlerInfo.
2. Se SMC falhar com "invalid user info", validar credenciais em SRO_ACCOUNT..TB_User.
3. Se cliente falhar em certification, validar IPs no server.cfg e Cert/ini/srNodeType.ini.

## Seguranca e boas praticas

- Evite manter credenciais em texto puro em ambientes de producao.
- Use senha forte para contas administrativas.
- Mantenha rotina de backup das bases e de Log/Archive.
- Registre toda mudanca em server.cfg e Cert/ini com data e motivo.

## Checklist rapido diario

1. Rodar _ops_health_check.cmd
2. Confirmar processos e portas criticas
3. Rodar _ops_rotate_logs.cmd
4. Verificar erros novos no FatalLog
5. Validar login no SMC
