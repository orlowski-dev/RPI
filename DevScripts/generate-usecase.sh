#!/usr/bin/env bash

set -e

MODULE=""
USECASE=""
GENERATE_DTO=false

while [[ $# -gt 0 ]]; do
	case "$1" in
	--module)
		MODULE="$2"
		shift 2
		;;
	--usecase)
		USECASE="$2"
		shift 2
		;;
	--dto)
		GENERATE_DTO=true
		shift
		;;
	*)
		echo "Nieznany argument: $1"
		exit 1
		;;
	esac
done

if [[ -z "$MODULE" || -z "$USECASE" ]]; then
	echo "Użycie:"
	echo "./generate-usecase.sh --module Dungeon --usecase EnterDungeon [--dto]"
	exit 1
fi

ROOT="./Game.Core/Application/$MODULE"

REQUEST_DIR="$ROOT/Requests"
RESPONSE_DIR="$ROOT/Responses"
USECASE_DIR="$ROOT/UseCases"
DTO_DIR="$ROOT/DTO"

mkdir -p \
	"$REQUEST_DIR" \
	"$RESPONSE_DIR" \
	"$USECASE_DIR"

if $GENERATE_DTO; then
	mkdir -p "$DTO_DIR"
fi

REQUEST_FILE="$REQUEST_DIR/${USECASE}Request.cs"
RESPONSE_FILE="$RESPONSE_DIR/${USECASE}Response.cs"
USECASE_FILE="$USECASE_DIR/${USECASE}UseCase.cs"
DTO_FILE="$DTO_DIR/${USECASE}Result.cs"

create_file_if_missing() {
	local file="$1"
	local content="$2"

	if [[ -f "$file" ]]; then
		echo "Pominięto: $file"
		return
	fi

	cat >"$file" <<EOF
$content
EOF

	echo "Utworzono: $file"
}

create_file_if_missing \
	"$REQUEST_FILE" \
	"public record ${USECASE}Request();"

if $GENERATE_DTO; then

	create_file_if_missing \
		"$DTO_FILE" \
		"public record ${USECASE}Result();"

	RESPONSE_CONTENT="public record ${USECASE}Response(${USECASE}Result result);"

else

	RESPONSE_CONTENT="public record ${USECASE}Response();"

fi

create_file_if_missing \
	"$RESPONSE_FILE" \
	"$RESPONSE_CONTENT"

create_file_if_missing \
	"$USECASE_FILE" \
	"public class ${USECASE}UseCase : IUseCase<${USECASE}Request, ${USECASE}Response>
{
    public Result<${USECASE}Response> Execute(${USECASE}Request req)
    {
        return Result<${USECASE}Response>.Success(new());
    }
}"

echo
echo "Wygenerowano ${USECASE} w module ${MODULE}"
if $GENERATE_DTO; then
	echo "Dodano DTO (${USECASE}Result)"
fi
