#!/usr/bin/env bash

set -e

MODULE=""
USECASE=""

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
        *)
            echo "Nieznany argument: $1"
            exit 1
            ;;
    esac
done

if [[ -z "$MODULE" || -z "$USECASE" ]]; then
    echo "Użycie:"
    echo "./generate-usecase.sh --module Dungeon --usecase EnterDungeon"
    exit 1
fi

ROOT="./Game.Core/Application/$MODULE"

REQUEST_DIR="$ROOT/Requests"
RESPONSE_DIR="$ROOT/Responses"
USECASE_DIR="$ROOT/UseCases"

mkdir -p \
    "$REQUEST_DIR" \
    "$RESPONSE_DIR" \
    "$USECASE_DIR"

REQUEST_FILE="$REQUEST_DIR/${USECASE}Request.cs"
RESPONSE_FILE="$RESPONSE_DIR/${USECASE}Response.cs"
USECASE_FILE="$USECASE_DIR/${USECASE}UseCase.cs"

create_file_if_missing() {
    local file="$1"
    local content="$2"

    if [[ -f "$file" ]]; then
        echo "Pominięto: $file"
        return
    fi

    cat > "$file" <<EOF
$content
EOF

    echo "Utworzono: $file"
}

create_file_if_missing \
"$REQUEST_FILE" \
"public record ${USECASE}Request();"

create_file_if_missing \
"$RESPONSE_FILE" \
"public record ${USECASE}Response();"

create_file_if_missing \
"$USECASE_FILE" \
"public class ${USECASE}UseCase : IUseCase<${USECASE}Request, ${USECASE}Response>
{
    public Result<${USECASE}Response> Execute(${USECASE}Request req)
    {
        var response = new ${USECASE}Response();
        return Result<${USECASE}Response>.Success(response);
    }
}"

echo
echo "Wygenerowano ${USECASE} w module ${MODULE}"
