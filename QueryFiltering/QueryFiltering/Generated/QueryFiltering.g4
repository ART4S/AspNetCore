grammar QueryFiltering;

options
{
	language=CSharp;
}

query
    :   (queryParameter ('&' queryParameter)*)?
    ;

queryParameter
    :   top|skip|filter|orderBy
    ;

top
    :   '$top=' count=INT
    ;

skip
    :   '$skip='count=INT
    ;

filter
    : '$filter=' expression=filterExpression
    ;

orderBy
    : '$orderBy=' expression=orderByExpression
    ;

filterExpression	
    :	atom ((OR|AND) atom)*
    ;

orderByExpression
    :   orderProperty (',' orderProperty)*
    ;

atom
    :   not=NOT? (boolExpr=boolExpression | '('filterExpr=filterExpression')')
    ; 

boolExpression
    : left=property operation=(EQUALS|NOTEQUALS|GREATERTHAN|GREATERTHANOREQUAL|LESSTHAN|LESSTHANOREQUAL) right=constant
    ;

property
    : value=PROPERTYACCESS
    ;

orderProperty
    :   value=PROPERTYACCESS op=(ASC|DESC)
    ;

constant
    : value=(INT|LONG|DOUBLE|FLOAT|DECIMAL|BOOL|NULL|GUID|STRING)
    ;

OR
    :   'or'
    ;
AND
    : 	'and'
    ;

NOT
    :   'not'
    ;

EQUALS
    :   'eq'
    ;	

NOTEQUALS
    :   'ne'
    ;

GREATERTHAN
    :   'gt'
    ;

GREATERTHANOREQUAL
    :   'ge'
    ;	

LESSTHAN
    :   'lt'
    ;

LESSTHANOREQUAL
    :   'le'
    ;

INT
    :    '-'? NUMBER+
    ;

LONG
    :	'-'? NUMBER+ 'l'
    ;

DOUBLE
    :   '-'? NUMBER+ ('.' NUMBER+)? 'd'
    ;	

FLOAT
    :   '-'? NUMBER+ ('.' NUMBER+)? 'f'
    ;

DECIMAL
    :   '-'? NUMBER+ ('.' NUMBER+)? 'm'
    ;	

BOOL
    :   'true'|'false'
    ;

GUID
    :   HEX_PAIR HEX_PAIR HEX_PAIR HEX_PAIR '-' HEX_PAIR HEX_PAIR '-' HEX_PAIR HEX_PAIR '-' HEX_PAIR HEX_PAIR '-' HEX_PAIR HEX_PAIR HEX_PAIR HEX_PAIR HEX_PAIR HEX_PAIR
    ;

NULL
    :   'null'
    ;

WHITESPACE 
    :   [ \t\r\n]+ -> skip
    ;

PROPERTYACCESS
    :   (PROPERTY ('.' PROPERTY)*)
    ;

ASC
    :   'asc'
    ;

DESC
    :   'desk'
    ;

STRING
    :   '\'' CHAR* '\''
    ;

CHAR
    :   '\'' ( ESC | ~('\''|'\\') ) '\''
    ;

fragment ESC
    :   '\\'(["\\/bfnrt]|UNICODE)
    ;

fragment UNICODE
    :   '\\' 'u' HEX HEX HEX HEX
    ;

fragment HEX_PAIR
    : HEX HEX
    ;

fragment HEX
    : [0-9a-fA-F] 
    ;

fragment PROPERTY
    : LETTER (LETTER|NUMBER|'_')*
    ;

fragment NUMBER
    : [0-9]
    ;

fragment LETTER
    : [a-zA-Z]
    ;