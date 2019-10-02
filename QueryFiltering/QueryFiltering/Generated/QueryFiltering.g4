grammar QueryFiltering;

options
{
	language=CSharp;
}

query
    :   ('?'? queryParameter ('&' queryParameter)*)?
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

orderBy
    : '$orderBy=' expression=orderByExpression
    ;

orderByExpression
    :   orderByProperty[true] (',' orderByProperty[false])*
    ;

orderByProperty[bool firstSort]
    :   value=PROPERTYACCESS op=(ASC|DESC)
    ;

filter
    : '$filter=' expression=filterExpression
    ;

filterExpression	
    :	filterAtom ((OR|AND) filterAtom)*
    ;

filterAtom
    :   not=NOT? (boolExpr=boolExpression | '(' filterExpr=filterExpression ')')
    ;

boolExpression
    : left=atom operation=(EQUALS|NOTEQUALS|GREATERTHAN|GREATERTHANOREQUAL|LESSTHAN|LESSTHANOREQUAL) right=atom
    ;

atom
    :   propertyValue=property
    |   constantValue=constant
    |   functionValue=function
    ; 

property
    : value=PROPERTYACCESS
    ;

constant
    : value=(INT|LONG|DOUBLE|FLOAT|DECIMAL|BOOL|NULL|GUID|STRING)
    ;

function
    :   value=(TOUPPER|TOLOWER|STARTSWITH|ENDSWITH)'(' atom (',' atom)* ')'
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

STRING
    :   '\'' ( ESC | ~('\''|'\\') )* '\''
    ;

PROPERTYACCESS
    :   (PROPERTY ('.' PROPERTY)*)
    ;

TOUPPER
    :   'toupper'
    ;

TOLOWER
    :   'tolower'
    ;

STARTSWITH
    :   'startswith'
    ;
	
ENDSWITH
    :   'endswith'
    ;

WHITESPACE 
    :   [ \t\r\n]+ -> skip
    ;

ASC
    :   'asc'
    ;

DESC
    :   'desk'
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